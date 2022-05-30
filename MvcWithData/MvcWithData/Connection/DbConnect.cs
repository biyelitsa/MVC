using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcWithData.Connection
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
    public static class DbConnect
    {
        static SqlConnection con;
        public static SqlConnection GetConnection()
        {
            if (con == null)
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
            }
            return con;
        }
    }
    
}