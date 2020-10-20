using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace BillAutomation
{
    public class DBHandler
    {
        public DBHandler(){}
        public SqlConnection GetConnection()
        {
            SqlConnection con = null;
            String connection = ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
            con = new SqlConnection(connection);
            return con;
            
        }
    }
}
