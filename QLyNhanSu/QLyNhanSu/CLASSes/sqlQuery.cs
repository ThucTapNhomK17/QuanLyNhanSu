using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace KhungGiaoDien
{
    public class sqlQuery
    {
        private static sqlQuery instance;

        public static sqlQuery Instance
        {
            get
            {
                if (instance == null)
                    instance = new sqlQuery();
                return sqlQuery.instance;
            }
            private set { sqlQuery.instance = value; }
        }
        public static SqlConnection m_SqlConnection;
        public static string m_ConnectionString = @"Data Source=SHOP\ICLICK;Initial Catalog=QuanLyNhanSu;Integrated Security=True";
        public sqlQuery()
        {
            try
            {
                if (m_SqlConnection != null && m_SqlConnection.State != ConnectionState.Open)
                {
                    m_SqlConnection.Open();
                }
                else if (m_SqlConnection == null)
                {
                    m_SqlConnection = new SqlConnection();
                    m_SqlConnection.ConnectionString = m_ConnectionString;
                    m_SqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool isConnectedToDatabase()
        {
            if (m_SqlConnection.State == ConnectionState.Open)
                return true;
            else
                return false;

        }
        public sqlQuery(String strConnect)
        {
            try
            {
                if (m_SqlConnection != null)
                    m_SqlConnection.Close();
                m_SqlConnection = new SqlConnection();
                m_SqlConnection.ConnectionString = strConnect;
                m_SqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DataTable Get_table(string query)
        {
            SqlDataAdapter sqldataAdapter = new SqlDataAdapter(query, m_SqlConnection);
            DataTable dt = new DataTable();
            try
            {
                if (m_SqlConnection.State != ConnectionState.Open)
                    m_SqlConnection.Open();

                sqldataAdapter.Fill(dt);//;, "CONFIG");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }
    }
}
