using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Services
{
    public class BaseService
    {
        private SqlConnection Connection;
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["assesmentDB"].ConnectionString;

        protected DataSet ExecuteQuery(string Query)
        {
            using(Connection = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(Query, Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        protected int ExecuteNonQuery(string Query)
        {
            using (Connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                int EffectedRows = cmd.ExecuteNonQuery();
                Connection.Close();
                return EffectedRows;
            }
        }
    }
}