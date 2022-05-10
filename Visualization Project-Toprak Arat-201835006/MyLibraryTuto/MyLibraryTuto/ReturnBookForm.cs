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
using System.Drawing.Printing;
namespace MyLibraryTuto
{
    public partial class ReturnBookForm : Form
    {
        public ReturnBookForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOPRAK\source\repos\MyLibraryTuto\Mylibrarydb.mdf;Integrated Security=True;Connect Timeout=30");
        public void populate()
        {
            Con.Open();
            string query = "select * from IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            IssueBookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        public void populatereturn()
        {
            Con.Open();
            string query = "select * from ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnedBookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void FillBook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qty>" + 0 + "", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", typeof(string));
            dt.Load(rdr);
            Bookcb.ValueMember = "BookName";
            Bookcb.DataSource = dt;
            Con.Close();
        }
        private void ReturnBookForm_Load(object sender, EventArgs e)
        {
            populate();
            populatereturn();
            FillBook();
        }

        private void IssueBookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //IssueNumTb.Text = IssueBookDGV.SelectedRows[0].Cells[0].Value.ToString();
            StdCb.SelectedItem = IssueBookDGV.SelectedRows[0].Cells[1].Value.ToString();
            stdnameTb.Text = IssueBookDGV.SelectedRows[0].Cells[2].Value.ToString();
            stddpmntTb.Text = IssueBookDGV.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTb.Text = IssueBookDGV.SelectedRows[0].Cells[4].Value.ToString();
            Bookcb.Text = IssueBookDGV.SelectedRows[0].Cells[5].Value.ToString();
        }
        private void UpdateBook()
        {
            int Qty, newQty;
            Con.Open();
            string query = "select * from BookTbl where BookName='" + Bookcb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty + 1;
                string query1 = "update BookTbl set Qty=" + newQty + " where BookName='" + Bookcb.SelectedValue.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ReturnNumTb.Text == "" || stdnameTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                string issuedate = IssueDate.Value.Day.ToString() + "/" + IssueDate.Value.Month.ToString() + "/" + IssueDate.Value.Year.ToString();
                string returndate = ReturnDate.Value.Day.ToString() + "/" + IssueDate.Value.Month.ToString() + "/" + IssueDate.Value.Year.ToString();
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ReturnTbl values(" + ReturnNumTb.Text + "," + StdCb.SelectedItem.ToString() + ",'" + stdnameTb.Text + "','" + stddpmntTb.Text + "','" + PhoneTb.Text + "','" + Bookcb.SelectedValue.ToString() + "','" + issuedate + "','"+returndate+"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfully Returned");
                Con.Close();
                UpdateBook();
                populate();
                populatereturn();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap,0,0);
          
        }
        Bitmap bitmap;
        private void button2_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
               this.Controls.Add(panel);
               Graphics graphics = panel.CreateGraphics();
               Size size = this.ClientSize;
            bitmap = new Bitmap(size.Width, size.Height, graphics);
               graphics = Graphics.FromImage(bitmap);
               Point point = PointToScreen(panel.Location);
               graphics.CopyFromScreen(point.X, point.Y, 0, 0, size);
               printPreviewDialog1.Document = printDocument1;
               printPreviewDialog1.ShowDialog();               
        }
    }
}
