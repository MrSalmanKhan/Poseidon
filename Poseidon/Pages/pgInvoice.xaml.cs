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
    /// Interaction logic for pgInvoice.xaml
    /// </summary>
    public partial class pgInvoice : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        Product product = new Product();
        List<InvoiceItem> invoiceList = new List<InvoiceItem>();

        public pgInvoice()
        {
            InitializeComponent();
        }
        private void btnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchStockItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StockItemsDialog dlg = new StockItemsDialog();
                dlg.ShowDialog();
                product = dlg.temp;
                PopulateTxtBoxes(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                btnSearchStockItem_Click(sender, e);
            }
        }

        private void PopulateTxtBoxes(Product product)
        {
            if (product != null)
            {
                txtProductName.Text = product.ProductName;
                txtGenericName.Text = product.GenericName;
                txtOrigin.Text = product.Origin;

                // lblCurrentQty.Content = "Current Qty: " + temp.Quantity.ToString();
                txtQuantity.Focus();
                btnDone.Visibility = Visibility.Hidden;
            }
        }

        private void btnAddToInvoiceList_Click(object sender, RoutedEventArgs e)
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
                InvoiceItem objinvoiceItem = new InvoiceItem();
                objinvoiceItem.StockItemId = product.Id;
                objinvoiceItem.Qty = Convert.ToInt32(txtQuantity.Text);
                objinvoiceItem.SellingPrice = Convert.ToInt32(txtPricePerUnit.Text);
                objinvoiceItem.Tax = Convert.ToInt32(txtTax.Text);
                objinvoiceItem.Discount = Convert.ToInt32(txtDiscount.Text);





            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgButton_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
            }
            catch(Exception ex)
            {
            }
        }

        private void ClearFields()
        {
            txtProductName.Text = string.Empty;
            txtGenericName.Text = string.Empty;
            txtOrigin.Text = string.Empty;
            txtTax.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtPricePerUnit.Text = string.Empty;
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
    }
}
