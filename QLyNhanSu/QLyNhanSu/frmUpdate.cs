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

namespace QLyNhanSu
{
    public partial class frmUpdate : Form
    {
        public frmUpdate()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (con.State != ConnectionState.Open)
                con.Open();
            sql = "update Emplyee set HoTen=N'" + txtHoTen.Text + "',NgaySinh='" + txtNgaySinh.Text + "',QueQuan=N'" + txtQueQuan.Text + "',Luong='" + txtLuong.Text + "' where ID='" + txtID.Text + "'";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
