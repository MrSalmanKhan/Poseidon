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
    /// Interaction logic for PgManageSoldStock.xaml
    /// </summary>
    public partial class PgManageSoldStock : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();

        int EditId = -1;
        public PgManageSoldStock()
        {
            InitializeComponent();
            this.RefreshDataGrid();
        }
        private void dgManageSoldStock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                EditId = dgManageSoldStock.Items.IndexOf(dgManageSoldStock.CurrentItem);
                if (EditId < 0) { return; }
                this.PopulateFields();
                txtQuantity.Focus();
                //txtQuantity.Select(0, txtQuantity.Text.Length);
                txtQuantity.SelectAll();
                btnSold.Visibility = Visibility.Hidden;
            }
            catch (Exception v) { MessageBox.Show(v.Message); }
        }
        private List<viewProductDetail> GetStockItems()
        {
            return db.viewProductDetails.Where(x => x.WarehouseNo == 1).ToList();
        }
        private void PopulateFields()
        {
            List<viewProductDetail> stockItems = this.GetStockItems();
            lblProductName.Content = String.Format("{0}", stockItems[EditId].ProductName);
            txtQuantity.IsEnabled = true;
            txtQuantity.Text = stockItems[EditId].Quantity.ToString();
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
                SoldItem Receiver = new SoldItem();
                viewProductDetail Sender = new viewProductDetail();

                Sender = GetStockItems()[EditId];

                if (int.Parse(txtQuantity.Text) > Sender.Quantity)
                {
                    MessageBox.Show("Mentioned Quantity is more than the total quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                this.Add(Sender, Receiver);

                this.RefreshDataGrid();
                this.ClearFields();
                btnSold.Visibility = Visibility.Visible;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.StackTrace); }
        }
        private void Add(viewProductDetail Sender, SoldItem Receiver)
        {
            // one row will be added in the Sold Items Table and the transferred product will be decreased
            Receiver.ProductName = Sender.ProductName;
            Receiver.Strength = Sender.Strength;
            Receiver.GenericName = Sender.GenericName;
            Receiver.Quantity = int.Parse(txtQuantity.Text);
            Receiver.PackSize = Sender.PackSize;
            Receiver.QtyPackSize = Sender.QtyPackSize;
            Receiver.ReorderLevel = Sender.ReorderLevel;
            Receiver.BatchNo = Sender.BatchNo;
            Receiver.ExpiryDate = Sender.ExpiryDate;
            Receiver.DateSold = dpDateSold.SelectedDate == null ? DateTime.Today : dpDateSold.SelectedDate;
            Receiver.Location = Sender.Location;
            Receiver.MajorSupplier = Sender.MajorSupplier;
            Receiver.Origin = Sender.Origin;
            Receiver.CostPerUnit = Sender.CostPerUnit;
            Receiver.TotalCost = Receiver.Quantity * Receiver.QtyPackSize * Receiver.CostPerUnit;

            Sender.Quantity = Sender.Quantity - int.Parse(txtQuantity.Text);
            Sender.TotalCost = Sender.Quantity * Sender.QtyPackSize * Sender.CostPerUnit;
            db.SaveChanges();

            db.SoldItems.Add(Receiver);
            db.SaveChanges();
        }
        //private void Update(StockItem Sender, SoldItem Receiver)
        //{
        //    // Both entities will be updated one will be increased other would be decreased
        //    Sender.Quantity = Sender.Quantity - int.Parse(txtQuantity.Text);
        //    Sender.TotalCost = Sender.Quantity * Sender.CostPerUnit;
        //    Receiver.Quantity = Receiver.Quantity + int.Parse(txtQuantity.Text);
        //    Receiver.TotalCost = Receiver.Quantity * Receiver.CostPerUnit;
        //    db.SaveChanges();
        //}

        private void RefreshDataGrid()
        {
            List<viewProductDetail> stockItems = db.viewProductDetails.Where(x => x.WarehouseNo == 1).ToList();
            dgManageSoldStock.ItemsSource = stockItems;
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
