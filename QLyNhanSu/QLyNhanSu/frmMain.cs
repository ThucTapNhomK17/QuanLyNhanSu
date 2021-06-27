using KhungGiaoDien;
using QLyNhanSu.CLASSes;
using QLyNhanSu.UCs;
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
    public partial class frmMain : Form
    {
        //public frmMain()
        //{
        //    InitializeComponent();
        //}
        private Acc loginAccount;
        public Acc LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }
        public frmMain(Acc acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            panel1.Controls.Clear();
            ucMain UCSV = new ucMain();
            panel1.Controls.Add(UCSV);
            UCSV.Dock = DockStyle.Fill;
        }
        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DataTable dt = truyVanDL.Get_table("SELECT id AS [ID], HoTen AS [Họ Tên], NgaySinh AS [Ngày Sinh], QueQuan AS [Quê Quán],Luong AS [LƯƠNG] FROM Employee");
            panel1.Controls.Clear();
            ucEmployee UCSV = new ucEmployee();
            panel1.Controls.Add(UCSV);
            UCSV.Dock = DockStyle.Fill;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            ucAcc UCAcc = new ucAcc();
            panel1.Controls.Add(UCAcc);
            UCAcc.Dock = DockStyle.Fill;
        }

        

        void ChangeAccount(int type)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Enabled = type == 1;
            //txtUserName.Text = LoginAccount.UserName;
            //txtDisplayName.Text = LoginAccount.DisplayName;
        }
    }
}
