using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace MyLibraryTuto
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOPRAK\source\repos\MyLibraryTuto\Mylibrarydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from BookTbl", Con);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
            Booklbl.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from StudentTbl", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            StudentLbl.Text = dt2.Rows[0][0].ToString();
            SqlDataAdapter sda3 = new SqlDataAdapter("select count(*) from LibrarianTbl", Con);
            DataTable dt3= new DataTable();
            sda3.Fill(dt3);
            LibrarianTbl.Text = dt3.Rows[0][0].ToString();
            SqlDataAdapter sda4 = new SqlDataAdapter("select count(*) from IssueTbl", Con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            IssuedLbl.Text = dt4.Rows[0][0].ToString();
            SqlDataAdapter sda5 = new SqlDataAdapter("select count(*) from ReturnTbl", Con);
            DataTable dt5 = new DataTable();
            sda5.Fill(dt5);
            ReturnLbl.Text = dt5.Rows[0][0].ToString();
            Con.Close();
        }
    }
}
