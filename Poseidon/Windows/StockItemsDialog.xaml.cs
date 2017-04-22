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
using System.Windows.Shapes;
using PoseidonShared.Model;

namespace Poseidon
{
    /// <summary>
    /// Interaction logic for StockItemsDialog.xaml
    /// </summary>
    public partial class StockItemsDialog : Window
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        public Product temp = new Product();
        public StockItemsDialog()
        {
            InitializeComponent();
            txtSearch.Focus();
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            dgStockItems.ItemsSource = db.Products.Take(200).ToList();
        }

        private void dgStockItems_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                temp = dgStockItems.SelectedItem as Product;
                if (temp != null)
                    this.Close();
                else
                    MessageBox.Show("No Product Selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                dgStockItems.ItemsSource = db.Products.Where(x => x.ProductName.Contains(txtSearch.Text)
                                                                     || x.GenericName.Contains(txtSearch.Text)
                                                                     || x.Origin.Contains(txtSearch.Text)).ToList();
            }
            if (txtSearch.Text.Length == 0)
                this.RefreshDataGrid();
        }
    }
}
