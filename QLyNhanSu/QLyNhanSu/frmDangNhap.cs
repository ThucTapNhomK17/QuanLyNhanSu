using QLyNhanSu.CLASSes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLyNhanSu
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string USERNAME = txtUserName.Text;
            string PASS = txtPassWord.Text;
            if (login(USERNAME, PASS))
            {
                Acc loginAccount = AccDAO.Instance.GetAccountByUserName(USERNAME);
                frmMain FrmMain = new frmMain(loginAccount);
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
            return AccDAO.Instance.login(USERNAME, PASS);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
