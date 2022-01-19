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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using static Library_System.Sql_Connection;

namespace Library_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            updatebook();
        }

       
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
           
            string LogUser = txtLoginUser.Text.Trim();
            string LogPassword = Encode.sha256_hash(pswLoginPassword.Password.Trim());
            DatabaseOperations.logincontrol(LogUser, LogPassword);
            if (GlobalVariables.loginRank == "1")
            {
                MessageBox.Show("Admin olarak başarılı giriş yapıldı");
                Admin.AdminWindow admin = new Admin.AdminWindow();
                admin.Show();
                this.Hide();
            }
            else if (GlobalVariables.loginRank == "3")
            {
                MessageBox.Show("Öğrenci olarak başarıyla giriş yapıldı.");
                Student.StudentWindow student = new Student.StudentWindow();
                student.Show();
                this.Hide();

            }

            else if (GlobalVariables.loginRank == "2")
            {
                MessageBox.Show("Öğretmen olarak başarıyla giriş yapıldı.");
                Student.StudentWindow student = new Student.StudentWindow();
                student.Show();
                this.Hide();

            }
            else if (GlobalVariables.loginRank ==null)
            {
                MessageBox.Show("Şifre veya kullanıcı adı hatalı");
            }



        }
        private void updatebook() 
        {
            

            SqlCommand commandread = new SqlCommand("Select *from AyarlarTablosuu", SqlConnections.con);


            if (commandread.Connection.State == System.Data.ConnectionState.Closed)
            {
                commandread.Connection.Open();
            }

            SqlDataReader reader = commandread.ExecuteReader();

            while (reader.Read())
            {
                GlobalVariables.booknumber = Convert.ToInt32(reader[1]);
                GlobalVariables.bookhistory = Convert.ToInt32(reader[2]);
               

            }

            reader.Close();

        }

        private void btnregister_Click(object sender, RoutedEventArgs e)
        {
            Register registerwindow = new Register();
            this.Hide();
            registerwindow.Show();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
    }

}
