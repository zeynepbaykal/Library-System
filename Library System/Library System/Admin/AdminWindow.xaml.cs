using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using static Library_System.Sql_Connection;

namespace Library_System.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void List()
        {
            string tempquery = "Select *from KütüphaneSistemiKİtapTablosu";

            dataGrid.ItemsSource = DatabaseOperations.ListQuery(tempquery).DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List();
        }

        private void btnekle_Click(object sender, RoutedEventArgs e)
        {
            List<object> templist = new List<Object>();
            templist.Add(txtboxad.Text);
            templist.Add(txtboxyazar.Text);
            templist.Add(txtboxsayfa.Text);
            templist.Add(txtboxkategori.Text);
            templist.Add(txtboxyıl.Text);
            templist.Add(txtboxadet.Text);



            string tempquery = "Insert into KütüphaneSistemiKİtapTablosu (KitapAdı,KitapYazarı,SayfaSayısı,KitapKategorisi,KitapYılı,KitapAdeti) values (@p1,@p2,@p3,@p4,@p5,@p6)";

            DatabaseOperations.ParameterizedQuery(tempquery, templist);

            List();
        }



        int id = -1;
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView dr = gd.SelectedItem as DataRowView;

                id = (int)dr[0];

                txtboxuad.Text = dr[1].ToString();
                txtboxuyazar.Text = dr[2].ToString();
                txtboxusayfa.Text = dr[3].ToString();
                txtboxukategori.Text = dr[4].ToString();
                txtboxuyıl.Text = dr[5].ToString();
                txtboxuadet.Text = dr[6].ToString();
            }
            catch (Exception)
            {


            }
        }

        private void btnsil_Click(object sender, RoutedEventArgs e)
        {
            List<object> list = new List<object>();

            list.Add(id);

            string tempquery = "Delete from KütüphaneSistemiKİtapTablosu where ID=@p1";

            DatabaseOperations.ParameterizedQuery(tempquery, list);

            List();
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            List<object> templist = new List<Object>();
            templist.Add(txtboxuad.Text);
            templist.Add(txtboxuyazar.Text);
            templist.Add(txtboxusayfa.Text);
            templist.Add(txtboxukategori.Text);
            templist.Add(txtboxuyıl.Text);
            templist.Add(txtboxuadet.Text);

            string tempquery = "Update KütüphaneSistemiKİtapTablosu set KitapAdı=@p1 ,KitapYazarı=@p2,SayfaSayısı=@p3, KitapKategorisi=@p4, KitapYılı=@p5,KitapAdeti=@p6";

            DatabaseOperations.ParameterizedQuery(tempquery, templist);

            List();
        }

        private void btnConfirmation_Click(object sender, RoutedEventArgs e)
        {
            BookReturnConfirmation bookReturnConfirmation = new BookReturnConfirmation();

            this.Hide();

            bookReturnConfirmation.Show();
        }

        private void teacherApproval_Click(object sender, RoutedEventArgs e)
        {
            TeacherApproval teacherApproval = new TeacherApproval();

            this.Hide();

            teacherApproval.Show();
        }

        private void btnAdminLogOut_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.loginUser = null;
            GlobalVariables.loginRank = null;
            lblUser.Content = "";
            MainWindow logout = new MainWindow();
            logout.Show();
            this.Hide();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();

            this.Hide();

            settings.Show();
        }


       


    }
}
