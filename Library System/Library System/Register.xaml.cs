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

namespace Library_System
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void savedata()
        {
         
            SqlCommand commandsave = new SqlCommand("Insert into KullanıcıGirişTablo (GirişBilgisi,Ad,Soyad,Şifre/*.Rank*/) Values (@girişbil,@adi,@soyadi,@şif/*,@rankk*/)", SqlConnections.con);

            if (commandsave.Connection.State == System.Data.ConnectionState.Closed)
            {
                commandsave.Connection.Open();
            }


            SqlCommand commandshow = new SqlCommand("Select *from KullanıcıGirişTablo", SqlConnections.con);


            SqlDataReader reader = commandshow.ExecuteReader();

            while (reader.Read())
            {
                if (reader["GirişBilgisi"].ToString() == txtRegKullanıcı.Text.Trim())
                {
                    MessageBox.Show("Böyle bir kullanıcı bulunmaktadır.");
                    
                    return;
                }
            }
            reader.Close();

            commandsave.Parameters.AddWithValue("@girişbil", txtRegKullanıcı.Text.Trim());
            commandsave.Parameters.AddWithValue("@adi", txtRegName.Text.Trim());
            commandsave.Parameters.AddWithValue("@soyadi", txtRegSurname.Text.Trim());
            commandsave.Parameters.AddWithValue("@şif", Encode.sha256_hash(pswPassword.Password.Trim()));
          

            commandsave.ExecuteNonQuery();

           

        }

        private void btnregisterScreen_Click(object sender, RoutedEventArgs e)
        {
            if (pswPassword.Password != pswRegPassword.Password)
            {
                MessageBox.Show("Şifreler uyuşmuyor.");
            }
            else
            {
                MessageBox.Show("Tebrikler kaydınız başarılı olmuştur.");
                savedata();
            }

        }

        private void btnReturn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Hide();
        }
    }
}

