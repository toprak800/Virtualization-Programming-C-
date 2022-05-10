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
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOPRAK\source\repos\MyLibraryTuto\Mylibrarydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from StudentTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (StdId.Text == "" || StdName.Text == "" || Stdphone.Text == "" || Stdsem.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into StudentTbl values(" + StdId.Text + ",'" + StdName.Text + "','" +StdDep.Text + "'," + Stdsem.SelectedItem.ToString()+",'"+Stdphone.Text+"')", Con);
                Console.WriteLine("insert into StudentTbl values(" + StdId.Text + ",'" + StdName.Text + "','" + StdDep.Text + "'," + Stdsem.SelectedItem.ToString() + ",'" + Stdphone.Text + "')");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Successfully");
                Con.Close();
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StdId.Text == "")
            {
                MessageBox.Show("Enter The Student Id");
            }
            else
            {
                Con.Open();
                string query = "delete from StudentTbl where StdId = " + StdId.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StdId.Text = StudentDGV.SelectedRows[0].Cells[0].Value.ToString();
            StdName.Text = StudentDGV.SelectedRows[0].Cells[1].Value.ToString();
            StdDep.Text = StudentDGV.SelectedRows[0].Cells[2].Value.ToString();
            Stdsem.SelectedItem = StudentDGV.SelectedRows[0].Cells[3].Value.ToString();
            Stdphone.Text = StudentDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StdId.Text == "" || StdName.Text == "" || Stdphone.Text == "" || Stdsem.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                string query = "update StudentTbl set StdName='" + StdName.Text + "',StdDep='" + StdDep.Text + "',StdSem=" + Stdsem.SelectedItem.ToString() + ",StdPhone='" + Stdphone.Text + "' where StdId =" + StdId.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully Updated");
                Con.Close();
                populate();
            }
        }
    }
}
