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
    /// Interaction logic for PgSupplier.xaml
    /// </summary>
    public partial class PgSupplier : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        Supplier objSupplier = new Supplier();
        public PgSupplier()
        {
            InitializeComponent();
            RefreshDataGrid();
            txtSupplierName.Focus();
        }

        //grid edit supplier 
        private void btnEditSupplier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                objSupplier = null;
                this.clearFields();
                objSupplier = (Supplier)dgSupplier.SelectedItem;
                txtSupplierName.Text = objSupplier.SupplierName;
                txtPhoneNo.Text = objSupplier.Phone;
                txtAddress.Text = objSupplier.Address;
                txtCountry.Text = objSupplier.Country;
                txtEmail.Text = objSupplier.Email;
                btnAdd.IsEnabled = false;
                btnEdit.Visibility = Visibility.Visible;
                EditLabel.Content = "Update in Progress,\n Click to cancel.";
                btnDone.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void clearFields()
        {
            try
            {
                txtSupplierName.Text = String.Empty;
                txtPhoneNo.Text = String.Empty;
                txtAddress.Text = String.Empty;
                txtCountry.Text = String.Empty;
                txtEmail.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Supplier row = (Supplier)dgSupplier.SelectedItem;

                if (row == null || row.Id < 1)
                    return;
                if (MessageBox.Show(String.Format("Delete '{0}' ?", row.SupplierName), "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    return;
                db.Suppliers.Remove(row);
                db.SaveChanges();
                this.RefreshDataGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RefreshDataGrid()
        {
            try
            {
                List<Supplier> lstSupplier = db.Suppliers.ToList();
                dgSupplier.ItemsSource = lstSupplier;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSupplierName.Text == "")
                {
                    MessageBox.Show("Please enter supplier name first.");
                    return;
                }
                var supplier = new Supplier();
                supplier.SupplierName = txtSupplierName.Text;
                supplier.Phone = txtPhoneNo.Text;
                supplier.Address = txtAddress.Text;
                supplier.Country = txtCountry.Text;
                supplier.Email = txtEmail.Text;
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                this.RefreshDataGrid();
                clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
       // public virtual bool Update(T entity)
        //{
            //Decimal id = ((dynamic)entity).Id;
            //var dbObject = ObjectSet.Where("it.Id=" + id).FirstOrDefault();
            //var entry = Db.Entry(dbObject);
            //entry.CurrentValues.SetValues(entity);
            //entry.State = EntityState.Modified;
            //Db.SaveChanges();
            //return true;
        //}
        private void dgSupplier_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void EditLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                objSupplier = null;
                btnAdd.IsEnabled = true;
                btnEdit.Visibility = Visibility.Hidden;
                EditLabel.Content = "";
                this.clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = objSupplier.Id;
                var result = db.Suppliers.SingleOrDefault(b => b.Id == id);
                result.SupplierName = txtSupplierName.Text;
                result.Phone = txtPhoneNo.Text;
                result.Address = txtAddress.Text;
                result.Country = txtCountry.Text;
                result.Email = txtEmail.Text;
                db.SaveChanges();
                RefreshDataGrid();
                clearFields();
                objSupplier = null;
                btnAdd.IsEnabled = true;
                btnEdit.Visibility = Visibility.Hidden;
                EditLabel.Content = "";

                //this.save()
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgSupplier_RowEditEnding_1(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                /// Handle Updates or Creates
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    // Get the Customer changes from the DataGrid
                    Supplier supplier = e.Row.DataContext as Supplier;


                    //if (String.IsNullOrEmpty(customerGroup.CustomerGroupName))
                    //{
                    //    MessageBox.Show("Must Provide a Customer Group Name", "Customer Group Name", MessageBoxButton.OK, MessageBoxImage.Information);
                    //    this.BindData();
                    //    return;
                    //}



                    if ((MessageBox.Show("Are you sure you want to add/modify '" + supplier.SupplierName + "' ?", "Confirm add or modify", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) != MessageBoxResult.Yes))
                    {
                        e.Cancel = true;
                        //this.RefreshDataGrid();
                        return;
                    }
                    //if (supplier.Id < 1)
                    //{
                    //    var Repo = new RepositorySupplier().Add(supplier);
                    //    if (Repo)
                    //    {
                    //        MessageBox.Show("Added successfully");
                    //    }

                    //    //ParaUtils.ShowStatusBarMsg(this, "Added CustomerGroup '" + customerGroup.CustomerGroupName + "'");
                    //    //this.ShowCustomerGroupDialog((from c in BobShared.BobSharedUtils.GetDBDataContext().CustomerGroups where c.CustomerGroupId == customerGroup.CustomerGroupId select c).SingleOrDefault());
                    //}
                    //else
                    //{
                    //    var Repo = new RepositorySupplier().Update(supplier);
                    //    if (Repo)
                    //    {
                    //        MessageBox.Show("Updated successfully");
                    //    }
                    //    //ParaUtils.ShowStatusBarMsg(this, "Updated Customer Group '" + customerGroup.CustomerGroupName + "'");
                    //}
                    // this.BindData();
                }
            }
            catch (Exception ex)
            {
                //  BobSharedUtils.HandleException(ex);
                MessageBox.Show(ex.Message);
                e.Cancel = true;
            }
        }



    }
}

