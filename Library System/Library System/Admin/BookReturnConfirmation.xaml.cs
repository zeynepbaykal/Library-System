using System;
using System.Collections.Generic;
using System.Data;
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

namespace Library_System.Admin
{
    /// <summary>
    /// Interaction logic for BookReturnConfirmation.xaml
    /// </summary>
    public partial class BookReturnConfirmation : Window
    {
        public BookReturnConfirmation()
        {
            InitializeComponent();
        }

        private void listBooks()
        {
            var tempQuery = "Select *from KitapİadeTablosuu ";

            DataTable dt = DatabaseOperations.ListQuery(tempQuery);

            dg.ItemsSource = dt.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBooks();
        }


        int id;
        string bookname;
        private void btnReturnConfirmation_Click(object sender, RoutedEventArgs e)
        {
            var tempQuery2 = "Delete from KitapİadeTablosuu where ID=@p1 ";


            List<object> list2 = new List<object>();

            list2.Add(id);

            DatabaseOperations.ParameterizedQuery(tempQuery2, list2);

            var tempQuery = "Update KütüphaneSistemiKİtapTablosu set KitapAdeti +=1 where KitapAdı=@p1 ";

            List<object> list = new List<object>();

            list.Add(bookname);

            DatabaseOperations.ParameterizedQuery(tempQuery ,list);



            listBooks();
        }

      
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var vRow = dg.SelectedItem;
            if (vRow == null)
            {
                return;
            }

            DataRowView dataRowView = (DataRowView)vRow;

            bookname = dataRowView.Row.ItemArray.GetValue(3).ToString();

            id = (int)dataRowView.Row.ItemArray.GetValue(0);
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();

            this.Hide();

            adminWindow.Show();
        }
    }
}
