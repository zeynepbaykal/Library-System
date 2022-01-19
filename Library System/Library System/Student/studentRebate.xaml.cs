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

namespace Library_System.Student
{
    /// <summary>
    /// Interaction logic for studentRebate.xaml
    /// </summary>
    public partial class studentRebate : Window
    {
        public studentRebate()
        {
            InitializeComponent();
        }

        private void listBooks()
        {
            var tempQuery = "Select *from AlınanKitapTablo where KitapAlanKullanıcı=@p1 ";

            List<object> list = new List<object>();

            list.Add(GlobalVariables.loginUser);

            DataTable dt = DatabaseOperations.ParameterizedQuery2(tempQuery,list);

            dataGrid2.ItemsSource = dt.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBooks();
        }

        int id;
        string bookname;
        private void btnrebate_Click(object sender, RoutedEventArgs e)
        {
            var tempQuery = "Insert into KitapİadeTablosuu (Kitapİade,KitapİadeEdenKullanıcı) values (@p1,@p2) ";

            List<object> list = new List<object>();

            list.Add(bookname);

            list.Add(GlobalVariables.loginUser);

            DataTable dt = DatabaseOperations.ParameterizedQuery2(tempQuery, list);

            dataGrid2.ItemsSource = dt.DefaultView;



            var tempQuery2 = "Delete from AlınanKitapTablo where ID=@p1 ";

            List<object> list2 = new List<object>();

            list2.Add(id);

            DatabaseOperations.ParameterizedQuery(tempQuery2, list2);

            listBooks();


        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            var vRow = dataGrid2.SelectedItem;
            if (vRow == null)
            {
                return;
            }

            DataRowView dataRowView = (DataRowView)vRow;

            bookname = dataRowView.Row.ItemArray.GetValue(3).ToString();

            id =(int) dataRowView.Row.ItemArray.GetValue(0);
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            StudentWindow studentWindow = new StudentWindow();

            this.Hide();

            studentWindow.Show();
        }
    }
}
