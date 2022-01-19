using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Library_System.Sql_Connection;

namespace Library_System
{
    class DatabaseOperations
    {
        public static void ParameterizedQuery(string tempquery, List<object> templist)
        {
            SqlCommand command = new SqlCommand(tempquery, SqlConnections.con);

            if (command.Connection.State == System.Data.ConnectionState.Closed)
            {
                command.Connection.Open();
            }

            int num = 1;
            int counter = 0;

            foreach (var p in templist)
            {
                command.Parameters.AddWithValue($"@p{num}", templist[counter]);
                num += 1;
                counter += 1;
            }

            command.ExecuteNonQuery();


        }

        public static DataTable ParameterizedQuery2(string tempquery, List<object> templist)
        {
            SqlCommand command = new SqlCommand(tempquery, SqlConnections.con);

            if (command.Connection.State == System.Data.ConnectionState.Closed)
            {
                command.Connection.Open();
            }

            int num = 1;
            int counter = 0;

            foreach (var p in templist)
            {
                command.Parameters.AddWithValue($"@p{num}", templist[counter]);
                num += 1;
                counter += 1;
            }

            SqlDataAdapter da = new SqlDataAdapter(command);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;


        }

        public static DataTable ListQuery(string tempquery)
        {
            SqlCommand commandlist = new SqlCommand(tempquery, SqlConnections.con);

            if (commandlist.Connection.State == System.Data.ConnectionState.Closed)
            {
                commandlist.Connection.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(commandlist);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }



        public static void logincontrol(string tempLogUser, string tempLogPassword) 
        {

            
            string commandtask = "Select *from KullanıcıGirişTablo where GirişBilgisi=@girişbil and Şifre=@şif";

            SqlCommand commandlog = new SqlCommand(commandtask, SqlConnections.con);

            if (commandlog.Connection.State == System.Data.ConnectionState.Closed)
            {
                commandlog.Connection.Open();
            }

            commandlog.Parameters.AddWithValue("@girişbil", tempLogUser);
            commandlog.Parameters.AddWithValue("@şif", tempLogPassword);
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandlog);

            dataAdapter.Fill(dataTable);
            SqlDataReader reader = commandlog.ExecuteReader();

            while (reader.Read())
            {
                if (dataTable.Rows.Count > 0)
                {
                    MessageBox.Show("Giriş Başarılı");
                    GlobalVariables.loginUser = tempLogUser;
                    GlobalVariables.loginRank = reader["Rank"].ToString();
                }

                else
                {
                    MessageBox.Show("Giriş Hatalı");
                    GlobalVariables.loginUser = null;
                    GlobalVariables.loginRank = null;

                    SqlConnections.con.Close();
                    return;

                }
            }
            reader.Close();

        }

        public static void bookcheck()
        {
            

            SqlCommand commandcheck = new SqlCommand("Select *from AlınanKitapTablo where KitapAlanKullanıcı=@bookuser", SqlConnections.con);


            if (commandcheck.Connection.State == System.Data.ConnectionState.Closed)
            {
                commandcheck.Connection.Open();
            }


            commandcheck.Parameters.AddWithValue("@bookuser", GlobalVariables.loginUser);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandcheck);

            DataTable datatable = new DataTable();

            dataAdapter.Fill(datatable);

            System.TimeSpan differencetime;

            if (datatable.Rows.Count > 0 && Convert.ToInt32(GlobalVariables.loginRank) == 3) 
            {
                int MyMeter = 0;

                foreach (DataRow dataRow in datatable.Rows)
                {
                    DateTime dateTime = Convert.ToDateTime(datatable.Rows[MyMeter][2]);
                    differencetime = DateTime.Now.Subtract(dateTime);
                    int difference = Convert.ToInt32(differencetime.Days);
                    if (Convert.ToInt32(difference) >= GlobalVariables.bookhistory)
                    {
                        string name = datatable.Rows[MyMeter][3].ToString().Trim();
                        MessageBox.Show($"{name} Kitabınızın Süresi Dolmuştur Lütfen İade ediniz.");
                    }
                    MyMeter += 1;
                }
            }
            
        }



    }
}
