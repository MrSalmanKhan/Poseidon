using System;
using System.Collections.Generic;
using System.Linq;
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
using PoseidonShared.Model;

namespace Poseidon.Pages
{
    /// <summary>
    /// Interaction logic for PgStockIn.xaml
    /// </summary>

    public partial class PgStockIn : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        List<viewProductDetail> viewStockList = new List<viewProductDetail>();
        List<StockItem> stockList = new List<StockItem>();
        StockItem tempItem = new StockItem();
        Product product = new Product();
        private NavigationService _navsvc;
        public PgStockIn()
        {
            try
            {
                InitializeComponent();
                txtProductName.Focus();
                this.Loaded += PgStockIn_Loaded;
                this.Unloaded += PgStockIn_Unloaded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddToDataGrid()
        {
            dgStockIn.ItemsSource = null;
            dgStockIn.ItemsSource = viewStockList;
        }

        private void DgButton_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                viewProductDetail row = (viewProductDetail)dgStockIn.SelectedItem;

                if (row == null)
                    return;
                 if (MessageBox.Show(String.Format("Delete '{0}' ?", row.ProductName), "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                     return;

                viewStockList.Remove(row);
                stockList.Remove(stockList.Where(x => x.ProductId == row.ProductId).Single());
                if (stockList.Count == 0)
                    btnAdd.Visibility = Visibility.Hidden;
                
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                MessageBox.Show("Please select valid row \n" + asd, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //this.Button_Click(null, e);
            }
        }

        private void btnSearchStockItem_Click(object sender, RoutedEventArgs e)
        {
            StockItemsDialog dlg = new StockItemsDialog();
            dlg.ShowDialog();
            product = dlg.temp;
            PopulateTxtBoxes(product);
        }

        private void PopulateTxtBoxes(Product product)
        {
            if (product != null)
            {
                txtProductName.Text = product.ProductName;
                txtGenericName.Text = product.GenericName;
                txtOrigin.Text = product.Origin;

                // lblCurrentQty.Content = "Current Qty: " + temp.Quantity.ToString();
                txtStrength.Focus();
                btnDone.Visibility = Visibility.Hidden;
            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                btnSearchStockItem_Click(sender, e);
            }
        }

        private void btnAddToStockList_Click(object sender, RoutedEventArgs e)
        {
            if (txtProductName.Text.Length < 1)
            {
                MessageBox.Show("Please select a product.", "Incomplete Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtQuantity.Text.Length < 1)
            {
                MessageBox.Show("Please Enter the required field 'Quantity'", "Incomplete Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtCostPerUnit.Text.Length < 1)
            {
                MessageBox.Show("Please Enter the required field 'CostPerUnit'", "Incomplete Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (cmbWarehouseNo.Text.Length < 1)
            {
                MessageBox.Show("Please Enter the required field 'Warehouse No.'", "Incomplete Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            viewProductDetail temp = new viewProductDetail();

            temp.ProductId = product.Id;
            temp.ProductName = product.ProductName;
            temp.GenericName = product.GenericName;
            temp.Origin = product.Origin;
            temp.Strength = txtStrength.Text.Length < 1 ? "" : txtStrength.Text;
            temp.Quantity = int.Parse(txtQuantity.Text); // required
            temp.PackSize = txtPackSize.Text.Length < 1 ? "" : txtPackSize.Text;
            temp.QtyPackSize = txtQtyPackSize.Text.Length < 1 ? 0 : int.Parse(txtQtyPackSize.Text);
            temp.ReorderLevel = txtReorderLevel.Text.Length < 1 ? 0 : int.Parse(txtReorderLevel.Text);
            temp.BatchNo = txtBatchNo.Text.Length < 1 ? "" : txtBatchNo.Text;
            temp.ExpiryDate = dpExpiryDate.SelectedDate;
            temp.Location = txtLocation.Text.Length < 1 ? "" : txtLocation.Text;
            temp.MajorSupplier = txtMajorSupplier.Text.Length < 1 ? "" : txtMajorSupplier.Text;
            temp.CostPerUnit = decimal.Parse(txtCostPerUnit.Text); // required
            temp.TotalCost = (Convert.ToDecimal(temp.Quantity)) * (Convert.ToDecimal(temp.QtyPackSize)) * temp.CostPerUnit;
            temp.WarehouseNo = int.Parse(cmbWarehouseNo.Text); // required

            StockItem stockItem = new StockItem();
            stockItem.ProductId = temp.ProductId;
            stockItem.Strength = temp.Strength;
            stockItem.Quantity = Convert.ToInt32(temp.Quantity);
            stockItem.PackSize = temp.PackSize;
            stockItem.QtyPackSize = temp.QtyPackSize;
            stockItem.ReorderLevel = temp.ReorderLevel;
            stockItem.BatchNo = temp.BatchNo;
            stockItem.ExpiryDate = temp.ExpiryDate;
            stockItem.Location = temp.Location;
            stockItem.MajorSupplier = temp.MajorSupplier;
            stockItem.CostPerUnit = temp.CostPerUnit;
            stockItem.TotalCost = temp.TotalCost;
            stockItem.WarehouseNo = Convert.ToInt32(temp.WarehouseNo);

            viewStockList.Add(temp);
            stockList.Add(stockItem);
            AddToDataGrid();
            ClearFields();
            btnAdd.Visibility = Visibility.Visible;
            btnDone.Visibility = Visibility.Hidden;
        }
        private void ClearFields()
        {
            txtProductName.Text = string.Empty;
            txtGenericName.Text = string.Empty;
            txtOrigin.Text = string.Empty;
            txtStrength.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtPackSize.Text = string.Empty;
            txtQtyPackSize.Text = string.Empty;
            txtReorderLevel.Text = string.Empty;
            txtBatchNo.Text = string.Empty;
            dpExpiryDate.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtMajorSupplier.Text = string.Empty;
            txtCostPerUnit.Text = string.Empty;
            cmbWarehouseNo.Text = string.Empty;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Stock stock = new Stock();
                stock.Timestamp = DateTime.Now;
                decimal grandTotal = 0;
                foreach (StockItem s in stockList)
                {
                    grandTotal = grandTotal + Convert.ToDecimal(s.TotalCost);
                }
                stock.GrandTotal = grandTotal;
                db.Stocks.Add(stock);
                db.SaveChanges();

                foreach (StockItem s in stockList)
                {
                    s.StockId = stock.Id;
                }

                db.StockItems.AddRange(stockList);
                db.SaveChanges();

                dgStockIn.ItemsSource = null;
                stockList = null;
                viewStockList = null;
                btnDone.Visibility = Visibility.Hidden;
                txtProductName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }

        void PgStockIn_Loaded(object sender, RoutedEventArgs e)
        {
            this._navsvc = this.NavigationService;
            this._navsvc.Navigating += NavigationService_Navigating;
        }

        void PgStockIn_Unloaded(object sender, RoutedEventArgs e)
        {
            this._navsvc.Navigating -= NavigationService_Navigating;
            this._navsvc = null;
        }

        void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (viewStockList.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to exit? The Stock is not saved in the Database yet, please click on the black Add Button to save.", "Confirm Exit", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    e.Cancel = true;
            }
        }
    }
}
