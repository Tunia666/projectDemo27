using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectDemo27
{
    internal class ConnectionSql
    {
        AuthForm authForm = new AuthForm();
        static string connectString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=demo27.1;";
        SqlConnection myConnection = new SqlConnection(connectString);
    }
}
