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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            PoseidonDBEntities db = new PoseidonDBEntities();
            if (txtName.Text == "" || txtPwd.Password == "")
            {
                lblAlert.Content = "Please fill all feilds";
            }
            else
            {
                var username = txtName.Text;
                var password = txtPwd.Password;
                var f = db.Accounts.Where(x => x.Username == username && x.Password == password).ToList();
                if (f.Count() > 0)
                {
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    lblAlert.Content = "Incorrect username and password";
                }
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
