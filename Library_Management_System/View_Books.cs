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
    public partial class View_Books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        public View_Books()
        {
            InitializeComponent();
        }

        private void View_Books_Load(object sender, EventArgs e)
        {

            Display_Books(); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
//Display.
        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select *from [Library_Management].[dbo].[Books_info] where Books_name like('%"+textBox1.Text+"%' )";
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i = int.Parse(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                con.Close();
                if (i == 0)
                {
                    MessageBox.Show("Books not Found!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            int i;
            i = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select *from [Library_Management].[dbo].[Books_info] where Id="+i+" ";
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    txtbookname.Text = dr["Books_name"].ToString();
                    txtauthname.Text = dr["Books_author_name"].ToString();
                    txtpubname.Text = dr["Books_publication_name"].ToString();
                    dateTimePicker1.Text = Convert.ToDateTime(dr["Books_purchase_date"]).ToString();
                    txtprice.Text = dr["Books_price"].ToString();
                    txtquantity.Text = dr["Books_quantity"].ToString();
                }

                con.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            i = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update [Library_Management].[dbo].[Books_info] set Books_name='" + txtbookname.Text + "',Books_author_name='" + txtauthname.Text + "',Books_publication_name='" + txtpubname.Text + "',Books_purchase_date='" + dateTimePicker1.Value + "',Books_price=" + txtprice.Text + ",Books_quantity=" + txtquantity.Text + " where Id="+i+"";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display_Books();
                MessageBox.Show("Updated Successfully");
                panel2.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Display_Books()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select *from [Library_Management].[dbo].[Books_info]";
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
