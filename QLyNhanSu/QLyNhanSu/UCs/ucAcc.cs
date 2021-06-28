using KhungGiaoDien;
using QLyNhanSu.CLASSes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLyNhanSu.UCs
{
    public partial class ucAcc : UserControl
    {
        BindingSource taiKhoanList = new BindingSource();
        
        
        public ucAcc()
        {
            InitializeComponent();

        }
        private Acc loginAccount;
        public Acc LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public ucAcc(Acc acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        void ChangeAccount(Acc acc)
        {
            txtUserName.Text = LoginAccount.UserName;
            txtDisplayName.Text = LoginAccount.DisplayName;
        }

        void UpdateAccountInfo()
        {
            string displayName = txtDisplayName.Text;
            string password = txtPassWord.Text;
            string newpass = txtNewPass.Text;
            string reenterPass = txtReEnterPass.Text;
            string userName = txtUserName.Text;

            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới!");
            }
            else
            {
                if (AccDAO.Instance.UpdateAccount(userName, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công");
                    if (updateAccount != null)
                        updateAccount(this, new AccountEvent(AccDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khấu");
                }
            }
        }

        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUserName.Clear();
            txtDisplayName.Clear();
            txtNewPass.Clear();
            txtPassWord.Clear();
            txtReEnterPass.Clear();
        }
        private void ucAcc_Load(object sender, EventArgs e)
        {
            dtgvAcc.DataSource = GetAccount().Tables[0];
        }
        DataSet GetAccount()
        {
            DataSet data = new DataSet();
            string query = "SELECT id AS [ID], DISPLAYNAME AS [Tên Hiển Thị], USERNAME AS [Tên Đăng Nhập], TYPE AS [Quyền Chỉnh Sửa] FROM ACC";
            using (SqlConnection connection = new SqlConnection(sqlQuery.m_ConnectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sqlQuery.m_ConnectionString);
            SqlDataAdapter aa = new SqlDataAdapter("PROC_INSERT_ACCOUNT", connection);
            try
            {
                connection.Open();
                aa.SelectCommand.CommandType = CommandType.StoredProcedure;
                aa.SelectCommand.Parameters.Add("@DisplayName", SqlDbType.NVarChar, (100)).Value = txtDisplayName.Text;
                aa.SelectCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, (100)).Value = txtUserName.Text;
                aa.SelectCommand.Parameters.Add("@Pass", SqlDbType.NVarChar, (1000)).Value = txtPassWord.Text;
                if (ckbType.CheckState==CheckState.Checked)
                {
                    aa.SelectCommand.Parameters.Add("@Type", SqlDbType.Int).Value = "1";
                }
                if (ckbType.CheckState==CheckState.Unchecked)
                {
                    aa.SelectCommand.Parameters.Add("@Type", SqlDbType.Int).Value = "0";
                }
                aa.SelectCommand.ExecuteNonQuery();
                connection.Close();
                dtgvAcc.DataSource = GetAccount().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtgvAcc_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SqlConnection connection = new SqlConnection(sqlQuery.m_ConnectionString);
            try
            {
                connection.Open();
                DataGridViewRow row = new DataGridViewRow();
                row = dtgvAcc.Rows[e.RowIndex];
                txtDisplayName.Text = row.Cells[1].Value.ToString();
                txtUserName.Text = row.Cells[2].Value.ToString();
                
                connection.Close();
            }
            catch (Exception)
            {

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sqlQuery.m_ConnectionString);
            SqlDataAdapter aa = new SqlDataAdapter("PROC_DELETE_ACCOUNT", connection);
            try
            {
                connection.Open();
                aa.SelectCommand.CommandType = CommandType.StoredProcedure;
                aa.SelectCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, (100)).Value = txtUserName.Text;
                
                aa.SelectCommand.ExecuteNonQuery();
                connection.Close();
                dtgvAcc.DataSource = GetAccount().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class AccountEvent : EventArgs
    {
        private Acc acc;

        public Acc Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        public AccountEvent(Acc acc)
        {
            this.Acc = acc;
        }
    }
}
