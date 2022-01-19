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
    /// Interaction logic for TeacherApproval.xaml
    /// </summary>
    public partial class TeacherApproval : Window
    {
        public TeacherApproval()
        {
            InitializeComponent();
        }

        private void saveInformation()
        {
         

            SqlCommand commandeditrank = new SqlCommand("Update KullanıcıGirişTablo  set Rank=2 where GirişBilgisi=@girişBilgisi", SqlConnections.con);

            if (commandeditrank.Connection.State == System.Data.ConnectionState.Closed)
            {
                commandeditrank.Connection.Open();
            }


            commandeditrank.Parameters.AddWithValue("@girişBilgisi", txtGiriş.Text.Trim());
            commandeditrank.ExecuteNonQuery();

            

        }
        private void btnSaveInformation_Click(object sender, RoutedEventArgs e)
        {
            saveInformation();

            MessageBox.Show("Değişiklikler başarıyla kayıt edildi.");
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            this.Hide();
        }
    }
}
