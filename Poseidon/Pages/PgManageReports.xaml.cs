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
using System.Globalization;

namespace Poseidon
{
    /// <summary>
    /// Interaction logic for PgManageReports.xaml
    /// </summary>
    public partial class PgManageReports : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        public PgManageReports()
        {
            InitializeComponent();
            //RefreshDataGrid();
            //LoadYears();
        }

        //private void SaleReportByMonthly(object sender, RoutedEventArgs e)
        //{
        //    //var EditIds = dgManageReports.SelectedItems.Count;
        //    if (cmbRptMonth.Text == "" || cmbRptYear.Text == "")
        //    {
        //        MessageBox.Show("Please select month and year", "Missing Requirements", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    else
        //    {
        //        //var rptList = new List<StockItem>();
        //        //var stockList = db.StockItems.GroupBy(c => c.ProductName).ToList();
        //        //for (int i = 0; i < EditIds; i++)
        //        //{
        //        //    rptList.Add((StockItem)dgManageReports.SelectedItems[i]);
        //        //}
        //        var mon = DateTime.ParseExact(cmbRptMonth.Text, "MMMM", CultureInfo.CurrentCulture).Month;
        //        var month = mon + "";
        //        var year = cmbRptYear.Text;
        //        var type = "";
        //        if (rdAll.IsChecked == true)
        //        {
        //            type = "all";
        //        }
        //        else if (rdWeekly.IsChecked == true)
        //        {
        //            type = "weekly";
        //        }
        //        else
        //        {
        //            type = "yearly";
        //        }
        //        new ReportViewerSale("", type, month, year).Show();
        //    }
       // }

        //private void RefreshDataGrid()
        //{
        //    var list = db.StockItems.GroupBy(c => c.ProductName).ToList();
        //    var newList = new List<StockItem>();
        //    foreach (var item in list)
        //    {
        //        var obj = item.FirstOrDefault();
        //        newList.Add(obj);
        //    }
        //    dgManageReports.ItemsSource = newList;
        //}
        //private void LoadYears()
        //{
        //    var year = DateTime.Now.Year;
        //    for (int i = 2015; i <= year; i++)
        //    {
        //        cmbRptYear.Items.Add(i);
        //    }
        //}

        //private void ReportMostSoldItems(object sender, RoutedEventArgs e)
        //{
        //    if (cmbRptMonth.Text == "" || cmbRptYear.Text == "")
        //    {
        //        MessageBox.Show("Please select month and year", "Missing Requirements", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    else
        //    {
        //        var mon = DateTime.ParseExact(cmbRptMonth.Text, "MMMM", CultureInfo.CurrentCulture).Month;
        //        var month = mon + "";
        //        var year = cmbRptYear.Text;
        //        var type = "";
        //        if (rdAll.IsChecked == true)
        //        {
        //            type = "all";
        //        }
        //        else if (rdWeekly.IsChecked == true)
        //        {
        //            type = "weekly";
        //        }
        //        else
        //        {
        //            type = "yearly";
        //        }
        //        new ReportViewerSale("most", type, month, year).Show();
        //    }
        //}

        //private void LeastSoldItems(object sender, RoutedEventArgs e)
        //{
        //    if (cmbRptMonth.Text == "" || cmbRptYear.Text == "")
        //    {
        //        MessageBox.Show("Please select month and year", "Missing Requirements", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    else
        //    {
        //        var mon = DateTime.ParseExact(cmbRptMonth.Text, "MMMM", CultureInfo.CurrentCulture).Month;
        //        var month = mon + "";
        //        var year = cmbRptYear.Text;
        //        var type = "";
        //        if (rdAll.IsChecked == true)
        //        {
        //            type = "all";
        //        }
        //        else if (rdWeekly.IsChecked == true)
        //        {
        //            type = "weekly";
        //        }
        //        else
        //        {
        //            type = "yearly";
        //        }
        //        new ReportViewerSale("least", type, month, year).Show();
        //    }
        //}

        //private void AllMonthlyStock(object sender, RoutedEventArgs e)
        //{
        //    if (cmbRptMonth.Text == "" || cmbRptYear.Text == "")
        //    {
        //        MessageBox.Show("Please select month and year", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    else
        //    {
        //        var mon = DateTime.ParseExact(cmbRptMonth.Text, "MMMM", CultureInfo.CurrentCulture).Month;
        //        var month = mon + "";
        //        var year = cmbRptYear.Text;
        //        new ReportViewerSale(month, year).Show();
        //    }
        //}
    }
}
