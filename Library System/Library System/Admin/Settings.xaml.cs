using System;
using System.Collections.Generic;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void changessettings()
        {
            

            SqlCommand commandsetting = new SqlCommand("Update AyarlarTablosuu set KitapSayısı=@booknumber , KitapVermeSüresi=@booktime where ID=0", SqlConnections.con);

            if (commandsetting.Connection.State == System.Data.ConnectionState.Closed)
            {
                commandsetting.Connection.Open();
            }

            commandsetting.Parameters.AddWithValue("@booknumber", txtBookReturn.Text);
            commandsetting.Parameters.AddWithValue("@booktime", txtBookTime.Text);

            commandsetting.ExecuteNonQuery();

            SqlCommand commandread = new SqlCommand("Select *from AyarlarTablosuu", SqlConnections.con);

            SqlDataReader reader = commandread.ExecuteReader();

            while (reader.Read())
            {
                GlobalVariables.booknumber = Convert.ToInt32(reader[1]);
                GlobalVariables.bookhistory = Convert.ToInt32(reader[2]);
            }

            reader.Close();

           

        }

        private void saveChangesSettings_Click(object sender, RoutedEventArgs e)
        {
            changessettings();
            MessageBox.Show("Ayarlar kaydedildi.");
        }

        private void btnPreviousSettings_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            this.Hide();
        }
    }
}
