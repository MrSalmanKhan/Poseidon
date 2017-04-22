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
    /// Interaction logic for PgRegister.xaml
    /// </summary>

    public partial class PgRegister : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        // int EditId = -1;
        public PgRegister()
        {
            try
            {
                InitializeComponent();
                RefreshDataGrid();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void RefreshDataGrid()
        {
            List<Product> products = db.Products.ToList();
            dgRegister.ItemsSource = products;
        }
        private void DgButton_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                Product row = (Product)dgRegister.SelectedItem;

                if (row == null || row.Id < 1)
                    return;
                if (MessageBox.Show(String.Format("Delete '{0}' ?", row.ProductName), "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    return;
                db.Products.Remove(row);
                db.SaveChanges();
                this.RefreshDataGrid();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Please select valid row.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        Product temp = new Product();
        private void DgButton_Edit(object sender, RoutedEventArgs e)
        {
            temp = (Product)dgRegister.SelectedItem;
            txtProductName.Text = temp.ProductName;
            txtGenericName.Text = temp.GenericName;
            txtOrigin.Text = temp.Origin;

            btnAdd.IsEnabled = false;
            btnEdit.Visibility = Visibility.Visible;
            EditLabel.Content = "Update in Progress,\n Click to cancel.";

        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            temp.ProductName = txtProductName.Text;
            temp.GenericName = txtGenericName.Text;
            temp.Origin = txtOrigin.Text;
            this.Save(temp);
            this.RefreshDataGrid();
            temp = null;
            btnAdd.IsEnabled = true;
            btnEdit.Visibility = Visibility.Hidden;
            EditLabel.Content = "";
            this.ClearFields();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductName = txtProductName.Text;
                product.GenericName = txtGenericName.Text;
                product.Origin = txtOrigin.Text;
                product.DateAdded = DateTime.Now;
                this.Save(product);
                this.RefreshDataGrid();
                this.ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }

        public void Save(Product product)
        {
            try
            {
                if (product == null)
                    return;
                if (product.Id < 1)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }

        private void EditLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            temp = null;
            btnAdd.IsEnabled = true;
            btnEdit.Visibility = Visibility.Hidden;
            EditLabel.Content = "";
            txtProductName.Text = "";
            txtGenericName.Text = "";
            txtOrigin.Text = "";
            this.ClearFields();
        }

        public void ClearFields()
        {
            txtProductName.Text = "";
            txtGenericName.Text = "";
            txtOrigin.Text = "";
        }
        private void btnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnAdd_Click(null, e);
            }
        }
    }
}
//private void dgMedicine_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
//{
//    try
//    {
//        //EditId = dgMedicine.Items.IndexOf(dgMedicine.CurrentItem);
//        //if (EditId == -1) { return; }

//    }
//    catch (Exception v) { MessageBox.Show(v.Message); }
//}
