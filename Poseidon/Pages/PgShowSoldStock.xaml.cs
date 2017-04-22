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
    /// Interaction logic for PgShowSoldStock.xaml
    /// </summary>
    public partial class PgShowSoldStock : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        public PgShowSoldStock()
        {
            InitializeComponent();
            this.RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            List<SoldItem> soldItems = db.SoldItems.ToList();
            dgManageSoldStock.ItemsSource = soldItems;
        }
    }
}
