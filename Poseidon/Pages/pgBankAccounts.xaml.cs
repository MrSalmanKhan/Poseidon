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

namespace Poseidon.Pages
{
    /// <summary>
    /// Interaction logic for pgBankAccounts.xaml
    /// </summary>
    public partial class pgBankAccounts : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        tblBankAccount objBankAccount = new tblBankAccount();
        public pgBankAccounts()
        {
            InitializeComponent();
            RefreshDataGrid();
            txtAccountTitle.Focus();
        }

        private void RefreshDataGrid()
        {
            try
            {
                List<tblBankAccount> lstBankAccounts = db.tblBankAccounts.ToList();
                dgAccounts.ItemsSource = lstBankAccounts;
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
                tblBankAccount row = (tblBankAccount)dgAccounts.SelectedItem;

                if (row == null || row.Id < 1)
                    return;
                if (MessageBox.Show(String.Format("Delete '{0}' ?", row.AccountTitle), "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    return;
                db.tblBankAccounts.Remove(row);
                db.SaveChanges();
                this.RefreshDataGrid();
            }
            catch
            {

            }
        }
        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtAccountTitle.Text == "")
                {
                    MessageBox.Show("Please enter account title first.");
                    txtAccountTitle.Focus();
                    return;
                }
                if(txtBankName.Text == "")
                {
                    MessageBox.Show("Please enter bank name first.");
                    txtBankName.Focus();
                    return;
                }
                var objBankAccount = new tblBankAccount();
                objBankAccount.AccountTitle = txtAccountTitle.Text;
                objBankAccount.BankName = txtBankName.Text;
                if(String.IsNullOrEmpty(dpDate.Text))
                {
                    dpDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                objBankAccount.Balance = Convert.ToInt32(txtBalance.Text);
                if (String.IsNullOrEmpty(txtBankName.Text))
                {
                    txtBankName.Text = "0";
                }
                objBankAccount.TimeStamp = Convert.ToDateTime(dpDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"));
                db.tblBankAccounts.Add(objBankAccount);
                db.SaveChanges();
                this.RefreshDataGrid();
                clearFields();


            }
            catch
            {

            }
        }


        private void btnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                objBankAccount = null;
                this.clearFields();
                objBankAccount = (tblBankAccount)dgAccounts.SelectedItem;
                txtAccountTitle.Text = objBankAccount.AccountTitle;
                txtBalance.Text = Convert.ToString(objBankAccount.Balance);
                txtBankName.Text = objBankAccount.BankName;
                dpDate.Text = objBankAccount.TimeStamp.ToString("yyyy-MM-dd");
                btnAdd.IsEnabled = false;
                btnEdit.Visibility = Visibility.Visible;
                EditLabel.Content = "Update in Progress,\n Click to cancel.";
                btnDone.Visibility = Visibility.Hidden;
            }
            catch
            {

            }
        }
        private void OnTimed(object sender, EventArgs e)
        {
            // Do something
        }

        private void dgAccounts_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {

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
                var id = objBankAccount.Id;
                var result = db.tblBankAccounts.SingleOrDefault(b => b.Id == id);
                result.AccountTitle = txtAccountTitle.Text;
                result.BankName = txtBankName.Text;
                result.Balance = Convert.ToInt32(txtBalance.Text);
                result.TimeStamp = Convert.ToDateTime(dpDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"));
                db.SaveChanges();
                RefreshDataGrid();
                clearFields();
                objBankAccount = null;
                btnAdd.IsEnabled = true;
                btnEdit.Visibility = Visibility.Hidden;
                EditLabel.Content = "";

            }
            catch
            {

            }
        }

        private void dgAccounts_RowEditEnding_1(object sender, DataGridRowEditEndingEventArgs e)
        {
        }
        private void dgCustomer_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch
            {
            }
        }

        private void clearFields()
        {
            try
            {
                txtAccountTitle.Text = String.Empty;
                txtBankName.Text = String.Empty;
                txtBalance.Text = String.Empty;
                dpDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                
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
                objBankAccount = null;
                btnAdd.IsEnabled = true;
                btnEdit.Visibility = Visibility.Hidden;
                EditLabel.Content = "";
                this.clearFields();
            }
            catch
            {

            }
        }



    }
}
