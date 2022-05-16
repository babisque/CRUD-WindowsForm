using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WF1_Exercicio.Service
{
    public class ConnectionCreator
    {
        public static SqlConnection CreateConnection()
        {
            string connectionString = @"Data Source=DESKTOP-16IKJ6C\SQLEXPRESS;Initial Catalog=Loja;Integrated Security=True";
            SqlConnection objCon = new SqlConnection(connectionString);

            return objCon;
        }
    }
}
