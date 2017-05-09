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
using PoseidonUI;
using Poseidon.Pages;

namespace Poseidon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PgHome());
        }
        private void navHome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgHome());
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Menu.Toggle();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void DefaultClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Default;
        }

        private void PrimaryClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Primary;
        }

        private void SuccessClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Success;
        }

        private void WarningClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Warning;
        }

        private void DangerClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Danger;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Menu.Hide();
        }

        private void ToggleClosingTypeClick(object sender, RoutedEventArgs e)
        {
            Menu.ClosingType = Menu.ClosingType == ClosingType.Auto 
                ? ClosingType.Manual 
                : ClosingType.Auto;
        }

        private SideMenu MapMenuToTheme(SideMenuTheme theme)
        {
            //this should not be necesray but colors are not changing correctly
            //when changing theme porperty... maybe its needed to implement INotifyPropertyChanged
            return new SideMenu
            {
                MenuWidth = Menu.MenuWidth,
                Theme = theme,
                Menu = Menu.Menu
            };
        }
        
        private void navSupplier_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgSupplier());
        }
        private void navCustomer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgCustomer());
        }
        private void navCuttentStock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgShowCurrentStock());
        }
        private void navRegister_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgRegister());
        }

        private void navInvoice_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new pgInvoice());
        }
        private void navStockIn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgStockIn());
        }
        private void navStockOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgStockOut());
        }
        private void navManageSoldStock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgManageSoldStock());
        }
        private void navShowSoldStock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgShowSoldStock());
        }

        private void OpenReportsPage(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgManageReports());
        }

        private void navStockAdjustment_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PgStockAdjustment());
        }

        private void navAccounts_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new pgBankAccounts());
        }

       
    }
}
