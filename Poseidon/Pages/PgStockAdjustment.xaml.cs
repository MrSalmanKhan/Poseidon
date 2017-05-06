using PoseidonShared.Model;
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

namespace Poseidon
{
    /// <summary>
    /// Interaction logic for PgStockAdjustment.xaml
    /// </summary>
    public partial class PgStockAdjustment : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        viewProductDetail rowToUpdate = new viewProductDetail();
        Product product = new Product();
        public PgStockAdjustment()
        {
            InitializeComponent();
            this.RefreshDataGrid();
            txtSearch.Text = "";
            txtSearch.Focus();
        }
        void RefreshDataGrid()
        {
            dgStockAdjustment.ItemsSource = null;
            dgStockAdjustment.ItemsSource = db.viewProductDetails.ToList();
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                dgStockAdjustment.ItemsSource = db.viewProductDetails.Where(x => x.ProductName.Contains(txtSearch.Text)
                                                                     || x.GenericName.Contains(txtSearch.Text)
                                                                     || x.Origin.Contains(txtSearch.Text)
                                                                     || x.BatchNo.Contains(txtSearch.Text)
                                                                     || x.Strength.Contains(txtSearch.Text)).ToList();
            }
            
            if (txtSearch.Text.Length == 0)
                this.RefreshDataGrid();
        }
        private void DgButton_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                viewProductDetail row = (viewProductDetail)dgStockAdjustment.SelectedItem;

                if (row == null)
                    return;
                if (MessageBox.Show(String.Format("Delete '{0}' ?", row.ProductName), "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    return;

                db.StockItems.Remove(db.StockItems.Where(x => x.Id == row.StockItemId).Single());
                db.SaveChanges();
                this.RefreshDataGrid();
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

                //lblCurrentQty.Content = "Current Qty: " + product.Quantity.ToString();
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


                StockItem stockItem = db.StockItems.Where(x => x.Id == rowToUpdate.StockItemId).Single();
                stockItem.ProductId = product.Id;
                stockItem.Strength = txtStrength.Text.Length < 1 ? "" : txtStrength.Text;
                stockItem.Quantity = int.Parse(txtQuantity.Text); // required
                stockItem.PackSize = txtPackSize.Text.Length < 1 ? "" : txtPackSize.Text;
                stockItem.QtyPackSize = txtQtyPackSize.Text.Length < 1 ? 0 : int.Parse(txtQtyPackSize.Text);
                stockItem.ReorderLevel = txtReorderLevel.Text.Length < 1 ? 0 : int.Parse(txtReorderLevel.Text);
                stockItem.BatchNo = txtBatchNo.Text.Length < 1 ? "" : txtBatchNo.Text;
                stockItem.ExpiryDate = dpExpiryDate.SelectedDate;
                stockItem.Location = txtLocation.Text.Length < 1 ? "" : txtLocation.Text;
                stockItem.MajorSupplier = txtMajorSupplier.Text.Length < 1 ? "" : txtMajorSupplier.Text;
                stockItem.CostPerUnit = decimal.Parse(txtCostPerUnit.Text); // required
                stockItem.TotalCost = (Convert.ToDecimal(stockItem.Quantity)) * (Convert.ToDecimal(stockItem.QtyPackSize)) * stockItem.CostPerUnit;
                stockItem.WarehouseNo = int.Parse(cmbWarehouseNo.Text); // required

                db.SaveChanges();
                
                this.RefreshDataGrid();
                //Stock stock = db.Stocks.Where(x => x.Id == stockItem.StockId).Single();
                //decimal grandTotal = 0;
                //foreach (StockItem s in stockList)
                //{
                //    grandTotal = grandTotal + Convert.ToDecimal(s.TotalCost);
                //}
                //stock.GrandTotal = grandTotal;
                //db.Stocks.Add(stock);
                //db.SaveChanges();

                //foreach (StockItem s in stockList)
                //{
                //    s.StockId = stock.Id;
                //}

                //db.StockItems.AddRange(stockList);
                //db.SaveChanges();

                //dgStockIn.ItemsSource = null;
                //stockList = null;
                //viewStockList = null;
                btnDone.Visibility = Visibility.Visible;
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }


        private void DgButton_Update(object sender, RoutedEventArgs e)
        {
            rowToUpdate = null;
            rowToUpdate = dgStockAdjustment.SelectedItem as viewProductDetail;
            
            txtProductName.Text = rowToUpdate.ProductName;
            txtGenericName.Text = rowToUpdate.GenericName;
            txtOrigin.Text = rowToUpdate.Origin;
            txtStrength.Text = rowToUpdate.Strength;
            txtQuantity.Text = rowToUpdate.Quantity.ToString();
            txtPackSize.Text = rowToUpdate.PackSize;
            txtQtyPackSize.Text = rowToUpdate.QtyPackSize.ToString();
            txtReorderLevel.Text = rowToUpdate.ReorderLevel.ToString();
            txtBatchNo.Text = rowToUpdate.BatchNo;
            dpExpiryDate.Text = rowToUpdate.ExpiryDate.ToString();
            txtLocation.Text = rowToUpdate.Location;
            txtMajorSupplier.Text = rowToUpdate.MajorSupplier;
            txtCostPerUnit.Text = rowToUpdate.CostPerUnit.ToString();
            cmbWarehouseNo.Text = rowToUpdate.WarehouseNo.ToString();
        }
    }
}
