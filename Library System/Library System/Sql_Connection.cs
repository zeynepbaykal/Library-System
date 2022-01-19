using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Library_System
{
    class Sql_Connection
    {
        public class SqlConnections
        {
            public static SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PSA6984\SQLEXPRESS;Initial Catalog=Toros;Integrated Security=True");
        }
    }
}
