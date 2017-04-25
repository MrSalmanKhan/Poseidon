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

namespace Poseidon
{
    /// <summary>
    /// Interaction logic for PgCustomer.xaml
    /// </summary>
    public partial class PgCustomer : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        Customer objCustomer = new Customer();
        public PgCustomer()
        {
            InitializeComponent();
            txtcustomerName.Focus();
            RefreshDataGrid();
        }

        private void dgbtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                objCustomer = new Customer();
                this.clearfields();
                objCustomer = (Customer)dgCustomer.SelectedItem;

                if (objCustomer == null || objCustomer.Id < 1)
                    return;

                txtcustomerName.Text = objCustomer.CustomerName;
                txtCustomerAge.Text = objCustomer.Age.ToString();
                txtCustomerAddress.Text = objCustomer.Address;
                txtCustomerCell.Text = objCustomer.Cell;
                txtCustomerEmail.Text = objCustomer.Email;
                txtCustomerGender.Text = objCustomer.Sex;
                txtCustomerPhone.Text = objCustomer.Phone;
                txtOrganization.Text = objCustomer.OrganizationName;
                dpRegistrationDate.Text = objCustomer.RegDate.ToString();
                txtCategory.Text = objCustomer.Category;
                txtBimaNo.Text = objCustomer.BimaNo;
                txtCredit.Text = objCustomer.Credit.ToString();
                txtDebit.Text = objCustomer.Debit.ToString();


                chkDOU.IsChecked = objCustomer.Dou;
                chkMedicalStore.IsChecked = objCustomer.MedicalStore;
                btnEdit.Visibility = Visibility.Visible;
                EditLabel.Content = "Update in Progress,\n Click to cancel.";
                btnAdd.IsEnabled = false;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgbtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer row = (Customer)dgCustomer.SelectedItem;

                if (row == null || row.Id < 1)
                    return;
                if (MessageBox.Show(String.Format("Delete '{0}' ?", row.CustomerName), "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    return;
                db.Customers.Remove(row);
                db.SaveChanges();
                this.RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void clearfields()
        {
            txtcustomerName.Text = String.Empty;
            txtCustomerAge.Text = String.Empty;
            txtCustomerGender.Text = String.Empty;
            txtCustomerAddress.Text = String.Empty;
            txtCustomerPhone.Text = String.Empty;
            txtCustomerCell.Text = String.Empty;
            txtCustomerEmail.Text = String.Empty;
            txtOrganization.Text = String.Empty;
            chkMedicalStore.IsChecked = false;
            chkDOU.IsChecked = false;
            txtBimaNo.Text = String.Empty;
            txtCategory.Text = String.Empty;
            dpRegistrationDate.Text = String.Empty;
            txtCredit.Text = String.Empty;
            txtDebit.Text = String.Empty;
        }

        private void RefreshDataGrid()
        {
            try
            {
                List<Customer> lstCstomer = db.Customers.ToList();
                dgCustomer.ItemsSource = lstCstomer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtcustomerName.Text == String.Empty)
                {
                    MessageBox.Show("Please enter customer name first.");
                    return;
                }
                objCustomer = new Customer();
                objCustomer.CustomerName = txtcustomerName.Text;
                objCustomer.Age =Convert.ToInt32( txtCustomerAge.Text);
                objCustomer.Sex = txtCustomerGender.Text;
       
                objCustomer.Address = txtCustomerAddress.Text;  //datatype = byte[]
                objCustomer.Phone = txtCustomerPhone.Text;
                objCustomer.Cell = txtCustomerCell.Text;
                objCustomer.Email = txtCustomerEmail.Text;
                objCustomer.OrganizationName = txtOrganization.Text;
                objCustomer.MedicalStore = Convert.ToBoolean(chkMedicalStore.IsChecked);   //datatype== bool
                objCustomer.Dou = Convert.ToBoolean(chkDOU.IsChecked);  // datatype bool
                objCustomer.BimaNo = txtBimaNo.Text;

                if (txtCredit.Text == String.Empty)
                {
                    txtCredit.Text = Convert.ToString(0);
                }
                if (txtDebit.Text == String.Empty)
                {
                    txtDebit.Text = Convert.ToString(0);
                }
                objCustomer.Credit = Convert.ToDecimal(txtCredit.Text);
                objCustomer.Debit = Convert.ToDecimal(txtDebit.Text);

                objCustomer.Category = txtCategory.Text;
                // String date = Convert.ToDateTime(txtRegDate.Text).ToString("dd-MMM-yyyy");
                String date = Convert.ToDateTime(dpRegistrationDate.Text).ToString("dd-MMM-yyyy");
                objCustomer.RegDate = Convert.ToDateTime(date);
                db.Customers.Add(objCustomer);
                db.SaveChanges();
                RefreshDataGrid();
                clearfields();
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
                var id = objCustomer.Id;

                var result = db.Customers.SingleOrDefault(b => b.Id == id);
                result.CustomerName = txtcustomerName.Text;
                result.Age = Convert.ToInt32(txtCustomerAge.Text);
                result.Address = txtCustomerAddress.Text;
                result.BimaNo = txtBimaNo.Text;
                result.Category = txtCategory.Text;
                result.Cell = txtCustomerCell.Text;
                result.Sex = txtCustomerGender.Text;
                result.Email = txtCustomerEmail.Text;
                result.MedicalStore = Convert.ToBoolean(chkMedicalStore.IsChecked);   //datatype== bool
                result.Dou = Convert.ToBoolean(chkDOU.IsChecked);
                result.Phone = txtCustomerPhone.Text;
                result.Category = txtCategory.Text;
                String date = Convert.ToDateTime(dpRegistrationDate.Text).ToString("dd-MMM-yyyy");
                result.RegDate = Convert.ToDateTime(date);
                if (txtCredit.Text == String.Empty)
                {
                    txtCredit.Text = Convert.ToString(0);
                }
                if (txtDebit.Text == String.Empty)
                {
                    txtDebit.Text = Convert.ToString(0);
                }
                result.Credit = Convert.ToDecimal(txtCredit.Text);
                result.Debit = Convert.ToDecimal(txtDebit.Text);

                db.SaveChanges();
                RefreshDataGrid();
                clearfields();
                objCustomer = null;
                btnAdd.IsEnabled = true;
                btnEdit.Visibility = Visibility.Hidden;
                EditLabel.Content = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void EditLabel_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            try
            {
                objCustomer = null;
                btnAdd.IsEnabled = true;
                btnEdit.Visibility = Visibility.Hidden;
                EditLabel.Content = "";
                this.clearfields();
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}

