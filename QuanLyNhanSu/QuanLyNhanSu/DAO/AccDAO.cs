using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSu.DAO
{
    public class AccDAO
    {
        private static AccDAO instance;

        public static AccDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private AccDAO()
        {
        }
        public bool login(string USERNAME, string PASS)
        {
            string query = "SELECT * FROM dbo.ACC WHERE USERNAME = N'" + USERNAME + "' AND PASS = N'" + PASS + "' ";
            DataTable result = dataprovider.Instance.Excutequery(query);
            return result.Rows.Count > 0;
        }
    }
}
