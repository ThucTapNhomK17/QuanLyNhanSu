using KhungGiaoDien;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLyNhanSu.CLASSes
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
            string query = "SELECT * FROM dbo.ACC WHERE USERNAME = '"+USERNAME+"' AND PASS = '"+PASS+"'";
            DataTable result = sqlQuery.Instance.Get_table(query);
            return result.Rows.Count > 0;
        }

        public Acc GetAccountByUserName(string userName)
        {
            DataTable data = sqlQuery.Instance.Get_table("SELECT * FROM dbo.ACC WHERE USERNAME = N'" + userName+"'");
            foreach (DataRow item in data.Rows)
            {
                return new Acc(item);
            }
            return null;
        }
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = sqlQuery.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { userName, displayName, pass, newPass });

            return result > 0;
        }
    }
}
