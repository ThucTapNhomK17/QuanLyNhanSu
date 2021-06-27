using KhungGiaoDien;
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
using QLyNhanSu.CLASSes;
namespace QLyNhanSu.UCs
{
    public partial class ucEmployee : UserControl
    {

        BindingSource nhanVienList = new BindingSource();
        public ucEmployee()
        {
            InitializeComponent();
           
        }
        private void ucEmployee_Load(object sender, EventArgs e)
        {
            dtgvEmployee.DataSource = GetNhanVien().Tables[0];
        }
        DataSet GetNhanVien()
        {
            DataSet data = new DataSet();
            string query = "SELECT id AS [ID], HoTen AS [Họ Tên], NgaySinh AS [Ngày Sinh], QueQuan AS [Quê Quán],Luong AS [LƯƠNG] FROM Employee";
            using (SqlConnection connection = new SqlConnection(sqlQuery.m_ConnectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sqlQuery.m_ConnectionString);
            SqlDataAdapter aa = new SqlDataAdapter("PROC_DELETE_EMPLOYEE", connection);
            try
            {
                connection.Open();
                aa.SelectCommand.CommandType = CommandType.StoredProcedure;
                aa.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, (10)).Value = txtID.Text;
                aa.SelectCommand.ExecuteNonQuery();
                connection.Close();
                dtgvEmployee.DataSource = GetNhanVien().Tables[0];
                txtID.Clear();
                txtHoTen.Clear();
                txtQueQuan.Clear();
                txtLuong.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dtgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int numrow;
            //numrow = e.RowIndex;
            SqlConnection connection = new SqlConnection(sqlQuery.m_ConnectionString);
            try
            {
                connection.Open();
                DataGridViewRow row = new DataGridViewRow();
                row = dtgvEmployee.Rows[e.RowIndex];
                txtID.Text = row.Cells[0].Value.ToString();
                txtHoTen.Text = row.Cells[1].Value.ToString();
                dateNgaySinh.Text = row.Cells[2].Value.ToString();
                txtQueQuan.Text = row.Cells[3].Value.ToString();
                txtLuong.Text = row.Cells[4].Value.ToString();
                connection.Close();
            }
            catch (Exception)
            {

            }
            
        }
        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sqlQuery.m_ConnectionString);
            SqlDataAdapter aa = new SqlDataAdapter("PROC_INSERT_EMPLOYEE", connection);
            try
            {
                connection.Open();
                aa.SelectCommand.CommandType = CommandType.StoredProcedure;
                aa.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = txtID.Text;
                aa.SelectCommand.Parameters.Add("@HOTEN", SqlDbType.NVarChar, (100)).Value = txtHoTen.Text;
                aa.SelectCommand.Parameters.Add("@NGAYSINH", SqlDbType.Date).Value = dateNgaySinh.Text;
                aa.SelectCommand.Parameters.Add("@QUEQUAN", SqlDbType.NVarChar, (200)).Value = txtQueQuan.Text;
                aa.SelectCommand.Parameters.Add("@LUONG", SqlDbType.Int).Value = txtLuong.Text;
                aa.SelectCommand.ExecuteNonQuery();
                connection.Close();
                dtgvEmployee.DataSource = GetNhanVien().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sqlQuery.m_ConnectionString);
            SqlDataAdapter aa = new SqlDataAdapter("PROC_UPDATE_EMPLOYEE", connection);
            try
            {
                connection.Open();
                aa.SelectCommand.CommandType = CommandType.StoredProcedure;
                aa.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = txtID.Text;
                aa.SelectCommand.Parameters.Add("@HOTEN", SqlDbType.NVarChar, (100)).Value = txtHoTen.Text;
                aa.SelectCommand.Parameters.Add("@NGAYSINH", SqlDbType.Date).Value = dateNgaySinh.Text;
                aa.SelectCommand.Parameters.Add("@QUEQUAN", SqlDbType.NVarChar, (200)).Value = txtQueQuan.Text;
                aa.SelectCommand.Parameters.Add("@LUONG", SqlDbType.Int).Value = txtLuong.Text;
                aa.SelectCommand.ExecuteNonQuery();
                connection.Close();
                dtgvEmployee.DataSource = GetNhanVien().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql = "SELECT id AS [ID], HoTen AS [Họ Tên], NgaySinh AS [Ngày Sinh], QueQuan AS [Quê Quán],Luong AS [LƯƠNG] FROM Employee ";
            string condition = "";
            if (txtSearchID.Text.Trim() != "")
            {
                condition += " id like '%" + txtSearchID.Text + "%'";
            }
            if (txtSearchName.Text.Trim() != "" && condition != "")
            {
                condition += " AND HoTen like N'%" + txtSearchName.Text + "%'";
            }
            if (txtSearchName.Text.Trim() != "" && condition == "")
            {
                condition += " HoTen like N'%" + txtSearchName.Text + "%'";
            }
            if (condition != "")
            {
                sql += " WHERE" + condition;
            }

            DataSet data = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlQuery.m_ConnectionString);
            adapter.Fill(data);
            dtgvEmployee.DataSource = data.Tables[0];
        }

    }
}
