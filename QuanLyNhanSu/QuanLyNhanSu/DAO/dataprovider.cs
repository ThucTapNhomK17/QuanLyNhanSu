using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSu.DAO
{
    class dataprovider
    {
        private static dataprovider instance;

        public static dataprovider Instance
        {
            get
            {
                if (instance == null)
                    instance = new dataprovider();
                return dataprovider.instance;
            }
            private set { dataprovider.instance = value; }
        }

        private string connectionSTR = @"Data Source=SHOP\ICLICK;Initial Catalog=QuanLyNhanSu;Integrated Security=True";

        public DataTable Excutequery(string query)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }
            return data;
        }
    }
}
