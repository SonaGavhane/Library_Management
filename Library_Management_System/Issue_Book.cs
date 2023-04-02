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

namespace Library_Management_System
{
    public partial class Issue_Book : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        public Issue_Book()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from [Library_Management].[dbo].[Student_info] where Student_enrollment_no='" + txtenroll.Text + "'";
          
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = int.Parse(dt.Rows.Count.ToString());
            if (i == 0)
            {
                MessageBox.Show("Record Not found");
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtname.Text = dr["Student_name"].ToString();
                    txtdep.Text = dr["Student_department"].ToString();
                    txtsem.Text = dr["Student_sem"].ToString();
                    txtcontact.Text = dr["Student_contact"].ToString();
                    txtemail.Text = dr["Student_email"].ToString();
                }
            }
           
        }

        private void Issue_Book_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void txtbookname_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;
            if (e.KeyCode != Keys.Enter)
            {
                listBox1.Items.Clear();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from [Library_Management].[dbo].[Books_info] where Books_name like('%" + txtbookname.Text + "%')";
                
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = int.Parse(dt.Rows.Count.ToString());
                if (count > 0)
                {
                    listBox1.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["books_name"].ToString());
                    }
                }
               
            }
        }

        private void txtbookname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;

            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbookname.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = false;
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            txtbookname.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int book_qty = 0;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            
            cmd2.CommandText = "select *from [Library_Management].[dbo].[Books_info] where Books_name='" + txtbookname.Text + "'";
            
            cmd2.ExecuteNonQuery();
            
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            da1.Fill(dt1);
            
            foreach (DataRow dr1 in dt1.Rows)
            {
                book_qty = int.Parse(dr1["Available_quantity"].ToString());
            }
            
            if (book_qty > 0)
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into [Library_Management].[dbo].[Issue_Book] values('" + txtname.Text + "','" + txtenroll.Text + "','" + txtdep.Text + "','" + txtsem.Text + "','" + txtcontact.Text + "','" + txtemail.Text + "','" + txtbookname.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "','')";
               
                cmd.ExecuteNonQuery();
                
                MessageBox.Show("data inserted successfully");



                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                
                cmd1.CommandText = "Update [Library_Management].[dbo].[Books_info] set Available_quantity=Available_quantity-1 where Books_name='" + txtbookname.Text + "'";
                cmd1.ExecuteNonQuery();
                
                MessageBox.Show("Data Updated Successfully");

            }
            else
            {
                MessageBox.Show("Books not available");
            }
        }
    }
}
