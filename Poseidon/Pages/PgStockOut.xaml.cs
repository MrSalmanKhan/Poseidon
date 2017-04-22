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
    /// Interaction logic for PgStockOut.xaml
    /// </summary>
    public partial class PgStockOut : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        viewProductDetail temp = new viewProductDetail();
        int EditId = -1;
        public PgStockOut()
        {
            InitializeComponent();
            this.RefreshDataGrid();
        }
        private void dgStockOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                EditId = dgStockOut.Items.IndexOf(dgStockOut.CurrentItem);
                if (EditId < 0) { return; }
                this.PopulateFields();
                txtQuantity.Focus();
                txtQuantity.SelectAll();
                btnDone.Visibility = Visibility.Hidden;
            }
            catch (Exception v) { MessageBox.Show(v.Message); }
        }
        private List<viewProductDetail> GetStockItems()
        {
            return db.viewProductDetails.ToList();
        }
        private void PopulateFields()
        {
            List<viewProductDetail> stockItems = this.GetStockItems();
            lblProductName.Content = String.Format("{0}", stockItems[EditId].ProductName);
            txtQuantity.IsEnabled = true;
            txtQuantity.Text = stockItems[EditId].Quantity.ToString();
            txtFromWarehouse.Text = stockItems[EditId].WarehouseNo.ToString();

        }
        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EditId == -1)
                {
                    MessageBox.Show("Please Select a row first.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                if (txtFromWarehouse.Text == cmbWarehouseNo2.Text)
                {
                    MessageBox.Show("Warehouses cannot be same.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                temp = GetStockItems()[EditId];

                if (int.Parse(txtQuantity.Text) > temp.Quantity)
                {
                    MessageBox.Show("Mentioned Quantity is more than the total quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                viewProductDetail Receiver = new viewProductDetail();
                if (GetStockItems().Where(x => x.WarehouseNo == int.Parse(cmbWarehouseNo2.Text) && x.ProductId == temp.ProductId).Count() > 0)
                {
                    Receiver = GetStockItems().Where(x => x.WarehouseNo == int.Parse(cmbWarehouseNo2.Text) && x.ProductName == temp.ProductName).FirstOrDefault();
                    // Product exists in the other warehouse therefore update operation
                    this.Update(db.StockItems.Where(x => x.Id == temp.StockItemId).SingleOrDefault(), db.StockItems.Where(x => x.Id == Receiver.StockItemId).SingleOrDefault());
                }
                else
                {
                    //Add Operation in the other warehouse
                    Receiver = temp;
                    this.Add(db.StockItems.Where(x => x.Id == temp.StockItemId).SingleOrDefault(), db.StockItems.Where(x => x.Id == Receiver.StockItemId).SingleOrDefault());

                }
                this.RefreshDataGrid();
                this.ClearFields();
                btnDone.Visibility = Visibility.Visible;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.StackTrace); }
        }
        private void Update(StockItem temp, StockItem Receiver)
        {
            // Both entities will be updated one will be increased other would be decreased
            temp.Quantity = temp.Quantity - int.Parse(txtQuantity.Text);
            temp.TotalCost = temp.Quantity * temp.CostPerUnit;

            Receiver.Quantity = Receiver.Quantity + int.Parse(txtQuantity.Text);
            Receiver.TotalCost = Receiver.Quantity * Receiver.QtyPackSize * Receiver.CostPerUnit;
            db.SaveChanges();
        }
        private void Add(StockItem temp, StockItem Receiver)
        {
            // one row will be added in the selected warehouse and the transferred product will be decreased
            temp.Quantity = temp.Quantity - int.Parse(txtQuantity.Text);
            temp.TotalCost = temp.Quantity * temp.CostPerUnit;
            db.SaveChanges();
            //Receiver.Id = -1;
            Receiver.WarehouseNo = int.Parse(cmbWarehouseNo2.Text);
            Receiver.Quantity = int.Parse(txtQuantity.Text);
            Receiver.TotalCost = Receiver.Quantity * Receiver.QtyPackSize * Receiver.CostPerUnit;
            db.StockItems.Add(Receiver);
            db.SaveChanges();
        }
        private void RefreshDataGrid()
        {
            dgStockOut.ItemsSource = null;
            List<viewProductDetail> stockItems = db.viewProductDetails.ToList();
            dgStockOut.ItemsSource = stockItems;
        }
        private void txtQuantity_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!txtQuantity.IsEnabled)
            {
                MessageBox.Show("Please Select a Product First.", "Select Product", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
            }

        }
        private void ClearFields()
        {
            cmbWarehouseNo2.Text = String.Empty;
            txtFromWarehouse.Text = String.Empty;
            txtQuantity.Text = String.Empty;
            lblProductName.Content = "[Product Name]";
        }
        private void btnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.btnTransfer_Click(null, e);
            }
        }

    }
}
