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
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

namespace نظام_مكتبة
{
    public partial class Form1 : Form
    {

        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\G.B\source\repos\نظام مكتبة\نظام مكتبة\book_info.mdf"";Integrated Security=True";

        SqlConnection con;
        DataTable dt;
        DataSet ds;
        SqlDataAdapter da;


        public Form1()
        {


            con = new SqlConnection(cs);
            ds = new DataSet();


            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }



        private void LoadData()
        {


            string query = "SELECT * FROM BOOKS";
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }
        private void cleaTextBoxes()
        {
            tbId.Clear();
            tbBook.Clear();
            tbAuthor.Clear();
            textBox4.Clear();
            tbLink.Clear();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbId.Text) ||

             string.IsNullOrWhiteSpace(tbBook.Text) ||
             string.IsNullOrWhiteSpace(tbAuthor.Text) ||
             string.IsNullOrWhiteSpace(tbLink.Text))
            {
                MessageBox.Show("يرحئ تعبيه جميع الحقول قبل الاضافه.");

                return;
            }
            string sql = "INSERT BOOKS SET BookName = @bookName, Author = @author ,Link = @Link WHERE Id = @id ";
            SqlCommand comm = new SqlCommand(sql, con);
          
            comm.Parameters.AddWithValue("@Id", tbId.Text);
            comm.Parameters.AddWithValue("@BookName", tbBook.Text);
            comm.Parameters.AddWithValue("@Author", tbAuthor.Text);
            comm.Parameters.AddWithValue("@Link", tbLink.Text);

            con.Open();
            comm.ExecuteNonQuery();
            con.Close();
            LoadData();
            MessageBox.Show("تمت الاضافة بنجاح");
            cleaTextBoxes();




        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(cs);
            string query = "select * from  BOOKS ";
            SqlCommand comm = new SqlCommand(query, con);
            con.Open();
            SqlDataReader DR = comm.ExecuteReader();
            dt = new DataTable();
            dt.Load(DR);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbId.Text) ||
         string.IsNullOrWhiteSpace(tbBook.Text) ||
         string.IsNullOrWhiteSpace(tbAuthor.Text) ||
       string.IsNullOrWhiteSpace(tbLink.Text))
            {
                MessageBox.Show("يرجئ تعبيه جميع الحقول .");
                return;
            }

            string query = "UPDATE BOOKS SET BookName = @bookName, Author = @author ,Link = @Link WHERE Id = @id ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", tbId.Text);
            cmd.Parameters.AddWithValue("@bookName", tbBook.Text);
            cmd.Parameters.AddWithValue("@author", tbAuthor.Text);
            cmd.Parameters.AddWithValue("@Link", tbLink.Text);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();

            if (rowsAffected > 0)
            {
                LoadData();
                MessageBox.Show(".تم تحديث بيانات الكتاب بنجاح");
                cleaTextBoxes();
            }
            else
            {
                MessageBox.Show(".حصل خطا . لم يتم التعديل");
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbId.Text))

            {
                MessageBox.Show(". الكتاب لحذفه id يرجئ اضافه");
                return;
            }

            string sql = "Delete from BOOKS where Id=@id";
            SqlCommand comm = new SqlCommand(sql, con);
            comm.Parameters.AddWithValue("@Id", tbId.Text);
            con.Open();
            comm.ExecuteNonQuery();
            con.Close();
            LoadData();
            MessageBox.Show("تم الحذف");
            cleaTextBoxes();


        }

        private void button4_Click(object sender, EventArgs e)
        {

            string st = "SELECT * FROM BOOKS WHERE  BookName LIKE @searsh";
            da = new SqlDataAdapter(st, con);
            da.SelectCommand.Parameters.AddWithValue("@searsh", "%" + textBox4.Text + "%");
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cleaTextBoxes();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                tbId.Text = row.Cells["Id"].Value.ToString();
                tbBook.Text = row.Cells["BookName"].Value.ToString();
                tbAuthor.Text = row.Cells["Author"].Value.ToString();
                tbLink.Text = row.Cells["Link"].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbId.Text) ||
             string.IsNullOrWhiteSpace(tbBook.Text) ||
             string.IsNullOrWhiteSpace(tbAuthor.Text) ||
             string.IsNullOrWhiteSpace(tbLink.Text))
            {
                MessageBox.Show("يرحئ تعبيه جميع الحقول قبل الاضافه.");
                return;
            }
            Process.Start(tbLink.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }

}

