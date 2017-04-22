using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfficeOpenXml;
using PoseidonShared.Model;
using System.Diagnostics;

namespace Poseidon.Pages
{
    /// <summary>
    /// Interaction logic for PgShowCurrentStock.xaml
    /// </summary>

    public partial class PgShowCurrentStock : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        public PgShowCurrentStock()
        {
            InitializeComponent();
            this.RefreshDataGrid(0);
            cmbWarehouseNo.Text = "Show All";
        }
        private void RefreshDataGrid(int WarehouseNo)
        {

            List<viewProductDetail> stockItems = new List<viewProductDetail>();
            if (WarehouseNo == 0)
            {
                dgManageSoldStock.ItemsSource = db.viewProductDetails.ToList();
                lblWarehouse.Content = "Showing Stock for All Warehouses";

            }
            else
            {
                stockItems = db.viewProductDetails.Where(x => x.WarehouseNo == WarehouseNo).ToList();
                dgManageSoldStock.ItemsSource = stockItems;
            }
        }

        private void cmbWarehouseNo_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbWarehouseNo.Text.Length == 1 || cmbWarehouseNo.Text == "Show All")
            {
                if (cmbWarehouseNo.Text != "Show All")
                {
                    this.RefreshDataGrid(int.Parse(cmbWarehouseNo.Text));
                    dgManageSoldStock.Columns[0].Visibility = Visibility.Collapsed;
                    lblWarehouse.Content = "Showing Stock for Warehouse " + cmbWarehouseNo.Text;
                }
                else if (cmbWarehouseNo.Text == "Show All")
                {
                    this.RefreshDataGrid(0);
                    dgManageSoldStock.Columns[0].Visibility = Visibility.Visible;
                }
            }
        }
        private void btnKeyDown(object sender, EventArgs e)
        { }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txtSearch.Text.Length > 0)
            {
                if (cmbWarehouseNo.Text != "Show All")
                {
                    int w_no = int.Parse(cmbWarehouseNo.Text);
                    dgManageSoldStock.ItemsSource = db.viewProductDetails.Where(x => x.ProductName.Contains(txtSearch.Text)
                                                                     || x.GenericName.Contains(txtSearch.Text)
                                                                     && x.WarehouseNo == w_no).ToList();
                    dgManageSoldStock.Columns[0].Visibility = Visibility.Visible;
                }
                else
                    dgManageSoldStock.ItemsSource = db.viewProductDetails.Where(x => x.ProductName.Contains(txtSearch.Text)
                                                                          || x.GenericName.Contains(txtSearch.Text)).ToList();

            }
            if (txtSearch.Text.Length < 1)
            {
                this.RefreshDataGrid(0);
            }
        }

        private string[] headers = new string[]
        {
            "Warehouse No",
            "Product Name",
            "Strength",
            "Generic Name",
            "Quantity",
            "Pack Size",
            "Qty Pack Size",
            "Reorder Level",
            "Batch No.",
            "Expiry Date",
            "Location ",
            "Major Supplier",
            "Origin",
            "Cost/Unit ",
            "Total Cost",

        };
        private string[] headers2 = new string[]
        {
            "Warehouse No",
            "Product Name",
            "Strength",
            "Generic Name",
            "Pack Size",
            "Qty Pack Size",
            "Reorder Level",
            "Batch No.",
            "Expiry Date",
            "Location ",
            "Major Supplier",
            "Origin",
            "Cost/Unit ",
            "Total Cost",

        };
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = System.IO.Path.GetTempPath();
                FileInfo newFile = new FileInfo(path + "\\" + "CurrentStock" + @".xlsx");

                if (newFile.Exists)
                {
                    newFile.Delete();
                    newFile = new FileInfo(path + "\\" + "CurrentStock" + @".xlsx");
                }
                ExcelPackage package = new ExcelPackage(newFile);
                var worksheet = package.Workbook.Worksheets.Add("Current Stock");
                //Add Header
                worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\"Current Stock";

                //using (var range1 = worksheet.Cells[10, 2, 10, 5])
                //{
                //    range.Style.Font.Bold = true;
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Teal);
                //    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                //}

                //Adding the headers
                int i = 0;
                foreach (string h in headers)
                {
                    worksheet.Cells[5, i + 1].Value = headers[i];
                    i++;
                }
                IReadOnlyList<viewProductDetail> StockList = db.viewProductDetails.OrderBy(x => x.WarehouseNo).ToList();
                int j = 6;
                int k = 0;
                //bool flag = true;
                while (StockList.Count > k)
                {
                    worksheet.Cells["A" + j].Value = StockList[k].WarehouseNo;
                    worksheet.Cells["B" + j].Value = StockList[k].ProductName;
                    worksheet.Cells["C" + j].Value = StockList[k].Strength;
                    worksheet.Cells["D" + j].Value = StockList[k].GenericName;
                    worksheet.Cells["E" + j].Value = StockList[k].Quantity;
                    worksheet.Cells["F" + j].Value = StockList[k].PackSize;
                    worksheet.Cells["G" + j].Value = StockList[k].QtyPackSize;
                    worksheet.Cells["H" + j].Value = StockList[k].ReorderLevel;
                    worksheet.Cells["I" + j].Value = StockList[k].BatchNo;
                    worksheet.Cells["J" + j].Value = StockList[k].ExpiryDate;
                    worksheet.Cells["K" + j].Value = StockList[k].Location;
                    worksheet.Cells["L" + j].Value = StockList[k].MajorSupplier;
                    worksheet.Cells["M" + j].Value = StockList[k].Origin;
                    worksheet.Cells["N" + j].Value = StockList[k].CostPerUnit;
                    worksheet.Cells["O" + j].Value = StockList[k].TotalCost;
                    j++;
                    k++;
                }
                //Add a formula for the value-column
                worksheet.Cells["O" + j].Formula = string.Format("SUM(O6:O{0})", j - 1);
                worksheet.Cells["N" + j].Formula = string.Format("SUM(N6:N{0})", j - 1);
                worksheet.Cells["G" + j].Formula = string.Format("SUM(G6:G{0})", j - 1);
                worksheet.Cells["E" + j].Formula = string.Format("SUM(E6:E{0})", j - 1);

                var range = worksheet.Cells[5, 1, j, 15];
                var table = worksheet.Tables.Add(range, "table1");
                //table.ShowTotal = true;
                table.TableStyle = OfficeOpenXml.Table.TableStyles.Light2;
                //table.Columns[2].TotalsRowFormula = "SUBTOTAL(109,[Column5])"; // sum

                var worksheet2 = package.Workbook.Worksheets.Add("Current Stock 2");
                this.Add2ndWorksheet(package, worksheet2, path);
                // set some document properties
                package.Workbook.Properties.Title = "Current Stock at " + DateTime.Today;
                package.Workbook.Properties.Author = "Pre-Poseidon a prequel of Poseidon";
                // package.Workbook.Properties.Comments = ;

                // set some extended property values
                package.Workbook.Properties.Company = "TechBros Pvt. Ltd.";

                // set some custom property values
                package.Workbook.Properties.SetCustomPropertyValue("Created By", "Salman Khan");
                package.Workbook.Properties.SetCustomPropertyValue("Email ", "mr.salman.rao@gmail.com");

                // save our new workbook and we are done!
                package.SaveAs(newFile);
                ProcessStartInfo StartInformation = new ProcessStartInfo();
                StartInformation.FileName = path + "\\" + "CurrentStock" + @".xlsx";
                Process process = Process.Start(StartInformation);
            }
            catch (Exception v) { MessageBox.Show(v.Message); }
        }

        private void Add2ndWorksheet(ExcelPackage package, ExcelWorksheet worksheet, string path)
        {

            int i = 0;
            foreach (string h in headers2)
            {
                worksheet.Cells[5, i + 1].Value = headers2[i];
                i++;
            }
            IReadOnlyList<viewProductDetail> StockList = db.viewProductDetails.OrderBy(x => x.WarehouseNo).ToList();
            int j = 6;
            int k = 0;
            //bool flag = true;
            while (StockList.Count > k)
            {
                worksheet.Cells["A" + j].Value = StockList[k].WarehouseNo;
                worksheet.Cells["B" + j].Value = StockList[k].ProductName;
                worksheet.Cells["C" + j].Value = StockList[k].Strength;
                worksheet.Cells["D" + j].Value = StockList[k].GenericName;
                worksheet.Cells["E" + j].Value = StockList[k].PackSize;
                worksheet.Cells["F" + j].Value = StockList[k].QtyPackSize;
                worksheet.Cells["G" + j].Value = StockList[k].ReorderLevel;
                worksheet.Cells["H" + j].Value = StockList[k].BatchNo;
                worksheet.Cells["I" + j].Value = StockList[k].ExpiryDate;
                worksheet.Cells["J" + j].Value = StockList[k].Location;
                worksheet.Cells["K" + j].Value = StockList[k].MajorSupplier;
                worksheet.Cells["L" + j].Value = StockList[k].Origin;
                worksheet.Cells["M" + j].Value = StockList[k].CostPerUnit;
                worksheet.Cells["N" + j].Value = StockList[k].TotalCost;
                j++;
                k++;
            }
            //Add a formula for the value-column
            worksheet.Cells["N" + j].Formula = string.Format("SUM(N6:N{0})", j - 1);
            worksheet.Cells["M" + j].Formula = string.Format("SUM(M6:M{0})", j - 1);
            worksheet.Cells["F" + j].Formula = string.Format("SUM(F6:F{0})", j - 1);

            var range = worksheet.Cells[5, 1, j, 14];
            var table = worksheet.Tables.Add(range, "table2");
            //table.ShowTotal = true;
            table.TableStyle = OfficeOpenXml.Table.TableStyles.Light2;
            //table.Columns[2].TotalsRowFormula = "SUBTOTAL(109,[Column5])"; // sum
        }
    }
}
