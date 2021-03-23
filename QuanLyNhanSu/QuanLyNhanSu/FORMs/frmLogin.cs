using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu.FORMs
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string USERNAME = txtUsername.Text;
            string PASS = txtPassword.Text;
            if (login(USERNAME, PASS))
            {
                frmMain FrmMain = new frmMain();
                this.Hide();
                FrmMain.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("sai ten tai khoan hoac mat khau");
            }
        }
        bool login(string USERNAME, string PASS)
        {
            return DAO.AccDAO.Instance.login(USERNAME, PASS);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
