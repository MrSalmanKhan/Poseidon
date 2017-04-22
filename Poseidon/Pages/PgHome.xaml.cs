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
    /// Interaction logic for PgHome.xaml
    /// </summary>
    public partial class PgHome : Page
    {
        PoseidonDBEntities db = new PoseidonDBEntities();
        List<viewProductDetail> Reorder = new List<viewProductDetail>();
        List<viewProductDetail> Expired = new List<viewProductDetail>();
        List<viewProductDetail> Finished = new List<viewProductDetail>();

        public PgHome()
        {
            InitializeComponent();
            LoadDatagrids();
        }
        private void LoadDatagrids()
        {
            try
            {
                Reorder = db.viewProductDetails.Where(x => x.ReorderLevel >= x.Quantity).ToList();
                dgReorderLevel.ItemsSource = Reorder;

                Expired = db.viewProductDetails.Where(x => x.ExpiryDate <= DateTime.Now).ToList();
                dgExpired.ItemsSource = Expired;
                DateTime d = DateTime.Today.AddDays(90);
                Finished = db.viewProductDetails.Where(x => x.ExpiryDate <= d && x.ExpiryDate > DateTime.Now).ToList();
                dgPrior90.ItemsSource = Finished;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Contact the Developer if this errors appears! \n" + ex.Message + "\n \n" + ex.InnerException);

            }
        }
        private void DgButton_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                viewProductDetail row = (viewProductDetail)dgExpired.SelectedItem;

                if (row == null || row.StockItemId < 1)
                    return;
                if (MessageBox.Show(String.Format("Delete '{0}' ?", row.ProductName), "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    return;
                db.StockItems.Remove(db.StockItems.Where(x => x.Id == row.StockItemId).SingleOrDefault());
                db.SaveChanges();
                this.LoadDatagrids();
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                MessageBox.Show("Please select valid row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
