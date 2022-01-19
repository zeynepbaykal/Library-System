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

namespace Library_System.Student
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public StudentWindow()
        {
            InitializeComponent();
            lblUserStudent.Content = GlobalVariables.loginUser;

        }

        private void btnLogOutStudent_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
           
            this.Hide();

            main.Show();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseOperations.bookcheck();
            listBooks();
        }

        private void chckbox_Checked(object sender, RoutedEventArgs e)
        {
            var tempQuery = "Select *from KütüphaneSistemiKİtapTablosu where KitapAdeti !=0";

            DataTable dt = DatabaseOperations.ListQuery(tempQuery);

            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void listBooks()
        {
            var tempQuery = "Select *from KütüphaneSistemiKİtapTablosu ";

            DataTable dt = DatabaseOperations.ListQuery(tempQuery);

            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void chckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            listBooks();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var tempQuery = "Select *from KütüphaneSistemiKitapTablosu where KitapAdı like '%" + txtBookName.Text + "%' and KitapKategorisi like '%" + txtBookType.Text + "%' and KitapYazarı like '%" + txtBookAuthor.Text + "%' and KitapAdı like '%" + txtCharacter.Text + "%'";

            DataTable dt = DatabaseOperations.ListQuery(tempQuery);


            dataGrid.ItemsSource = dt.DefaultView;

        }

        int id;

        string bookname;

        int kitapadeti;

       

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vRow = dataGrid.SelectedItem;
            if (vRow == null)
            {
                return;
            }

            DataRowView dataRowView = (DataRowView)vRow;

            id = (int)dataRowView.Row.ItemArray.FirstOrDefault();

            bookname = dataRowView.Row.ItemArray.GetValue(1).ToString();

            kitapadeti = (int)dataRowView.Row.ItemArray.GetValue(6);
        }

        private void buybook()
        {
           

            SqlCommand commandread = new SqlCommand("Select *from KütüphaneSistemiKitapTablosu where ID=@idi", SqlConnections.con);

            if (commandread.Connection.State == System.Data.ConnectionState.Closed)
            {
                commandread.Connection.Open();
            }

            commandread.Parameters.AddWithValue("@idi", Convert.ToInt32(txtBookId.Text));

            SqlDataReader reader = commandread.ExecuteReader();

            while (reader.Read())
            {
                GlobalVariables.bookname = reader[1].ToString();

                int booknumber = Convert.ToInt32(reader[6]);

                if (booknumber == 0)
                {
                    MessageBox.Show("İstediğiniz Kitap Şuanda Kullanıcıdadır.");
                    if (commandread.Connection.State == System.Data.ConnectionState.Closed)
                    {
                        commandread.Connection.Open();
                    }
                    return;
                }
            }

            reader.Close();

            SqlCommand commandbooknumber = new SqlCommand("Select *from AlınanKitapTablo where KitapAlanKullanıcı=@bookuser", SqlConnections.con); 

            commandbooknumber.Parameters.AddWithValue("@bookuser", GlobalVariables.loginUser);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandbooknumber);

            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            if (Convert.ToInt32(dataTable.Rows.Count) >= GlobalVariables.booknumber && Convert.ToInt32(GlobalVariables.loginRank) == 3) 
            {
                MessageBox.Show($"Kullanıcı En Fazla {GlobalVariables.booknumber}. Kitap Alabilir.");

                SqlConnections.con.Close();

                return;
            }

            SqlCommand commandpiece = new SqlCommand("Update KütüphaneSistemiKitapTablosu set KitapAdeti-=1 where KitapAdı=@bookname", SqlConnections.con);

            commandpiece.Parameters.AddWithValue("@bookname", GlobalVariables.bookname);

            SqlCommand commandlistadd = new SqlCommand("Insert into AlınanKitapTablo (KitapAlanKullanıcı, KitapAlınanTarih, AlınanKitabınAdı) values (@bookuser, @bookdate, @bookname)", SqlConnections.con);

            commandlistadd.Parameters.AddWithValue("@bookuser", GlobalVariables.loginUser);

            commandlistadd.Parameters.AddWithValue("@bookdate", DateTime.Now);

            commandlistadd.Parameters.AddWithValue("@bookname", GlobalVariables.bookname);

            commandlistadd.ExecuteNonQuery();

            commandpiece.ExecuteNonQuery();

            SqlConnections.con.Close();

            MessageBox.Show("Kitap Başarıyla Alındı");


        }
        private void btnBookTake_Click(object sender, RoutedEventArgs e)
        {
           
            buybook();
        }

        private void btnBookReceived_Click(object sender, RoutedEventArgs e)
        {
            studentRebate studentRebate = new studentRebate();
            this.Hide();
            studentRebate.Show();
        }

        
    }
}
