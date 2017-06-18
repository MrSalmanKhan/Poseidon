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
       public viewProductDetail tempstockItem = new viewProductDetail();
       // Product product = new Product();
        List<InvoiceItem> invoiceList = new List<InvoiceItem>();
        List<InvoiceView> viewInvoiceList = new List<InvoiceView>();

        public pgInvoice()
        {
            InitializeComponent();
            txtProductName.Focus();
            btnAdd.Visibility = Visibility.Hidden;

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
                StockItemsInvoiceDialog dlg = new StockItemsInvoiceDialog();
                dlg.ShowDialog();
                tempstockItem = dlg.temp;
                PopulateTxtBoxes(tempstockItem);
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

        private void PopulateTxtBoxes(viewProductDetail product)
        {
            if (product != null)
            {
                txtProductName.Text = product.ProductName;
                txtGenericName.Text = product.GenericName;
                txtOrigin.Text = product.Origin;
                txtAvailableQuantity.Text = Convert.ToString(product.Quantity);

                // lblCurrentQty.Content = "Current Qty: " + temp.Quantity.ToString();
                txtQuantity.Focus();
                btnDone.Visibility = Visibility.Hidden;
            }
        }

        private void AddToDataGrid()
        {
            dgInvoice.ItemsSource = null;
            dgInvoice.ItemsSource = viewInvoiceList;
        }

        private void btnAddToInvoiceList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtQuantity.Text) > Convert.ToInt32(txtAvailableQuantity.Text))
                {
                    MessageBox.Show("Quantity must be less than or equal to available quantity.","", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;

                }

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
                //InvoiceItem objinvoiceItem = new InvoiceItem();
                ////objinvoiceItem.StockItemId = product.Id;
                //objinvoiceItem.Qty = Convert.ToInt32(txtQuantity.Text);
                //objinvoiceItem.SellingPrice = Convert.ToInt32(txtPricePerUnit.Text);
                //objinvoiceItem.Tax = Convert.ToInt32(txtTax.Text);
                //objinvoiceItem.Discount = Convert.ToInt32(txtDiscount.Text);
                InvoiceView objviewInvoice = new InvoiceView();
                objviewInvoice.StockItemId = tempstockItem.StockItemId;
                objviewInvoice.Qty = Convert.ToInt32(txtQuantity.Text);
                objviewInvoice.ProductName = txtProductName.Text;
                objviewInvoice.Origin = txtOrigin.Text;
                objviewInvoice.GenericName = txtGenericName.Text;
                objviewInvoice.SellingPrice = Convert.ToInt32(txtPricePerUnit.Text);
                objviewInvoice.Tax = Convert.ToInt32(txtTax.Text);
                objviewInvoice.Discount = Convert.ToInt32(txtDiscount.Text);


                InvoiceItem objinvoiceItem = new InvoiceItem();
                objinvoiceItem.StockItemId = objviewInvoice.StockItemId;
                objinvoiceItem.Qty = objviewInvoice.Qty;
                objinvoiceItem.SellingPrice = objviewInvoice.SellingPrice;
                objinvoiceItem.Tax = objviewInvoice.Tax;
                objinvoiceItem.Discount = objviewInvoice.Discount;

                viewInvoiceList.Add(objviewInvoice);
                invoiceList.Add(objinvoiceItem);
                AddToDataGrid();
                ClearFields();
                btnAdd.Visibility = Visibility.Visible;
                btnDone.Visibility = Visibility.Hidden;





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgButton_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                InvoiceView row = (InvoiceView)dgInvoice.SelectedItem;

                if (row == null)
                    return;
                if (MessageBox.Show(String.Format("Delete '{0}' ?", row.ProductName), "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    return;

                viewInvoiceList.Remove(row);
                invoiceList.Remove(invoiceList.Where(x => x.StockItemId == row.StockItemId).Single());
                if (invoiceList.Count == 0)
                    btnAdd.Visibility = Visibility.Hidden;
                AddToDataGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                Invoice invoice = new Invoice();
                invoice.TimeStamp = DateTime.Now;
                Decimal TotalTax = 0;
                Decimal TotalDiscount = 0;
                Decimal TotalPrice = 0;
                foreach(InvoiceItem i in invoiceList)
                {
                    TotalTax = TotalTax + Convert.ToDecimal(i.Tax);
                    TotalPrice = TotalPrice + Convert.ToDecimal(i.SellingPrice);
                    TotalDiscount = TotalDiscount + Convert.ToDecimal(i.Discount);
                }
                invoice.TotalDiscount = TotalDiscount;
                invoice.TotalTax = TotalTax;
                invoice.TotalAmount = TotalPrice;
                db.Invoices.Add(invoice);
                db.SaveChanges();

                foreach (InvoiceItem i in invoiceList)
                {
                    i.InvoiceId = invoice.Id;
                    StockItem updateStockItemQty = db.StockItems.Where(x => x.Id == i.StockItemId ).Single();
                    if(updateStockItemQty != null)
                    { 
                    updateStockItemQty.Quantity = updateStockItemQty.Quantity - i.Qty;
                    db.SaveChanges();
                    }

                    db.InvoiceItems.Add(i);

                }
                db.SaveChanges();
                dgInvoice.ItemsSource = null;
                invoiceList = null;
                viewInvoiceList = null;
                btnDone.Visibility = Visibility.Visible;
                txtProductName.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
