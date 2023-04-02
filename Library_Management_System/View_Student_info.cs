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
    public partial class View_Student_info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        public View_Student_info()
        {
            InitializeComponent();
        }

        private void View_Student_info_Load(object sender, EventArgs e)
        {
            Display_Student();
        }
        public void Display_Student()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select *from [Library_Management].[dbo].[Student_info]";
                con.Open();
                cmd.ExecuteNonQuery();
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
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                try
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.Refresh();
                   
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "select *from [Library_Management].[dbo].[Student_info] where Student_name like('%"+textBox1.Text+"%')";
                    con.Open();
                    cmd.ExecuteNonQuery();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from[Library_Management].[dbo].[Student_info] where Id=" + i +" ";
            con.Open();
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtname.Text = dr["Student_name"].ToString();
                txtadd.Text = dr["Student_address"].ToString();
                txtenroll.Text = dr["Student_enrollment_no"].ToString();
                txtdep.Text = dr["Student_department"].ToString();
                txtsem.Text = dr["Student_sem"].ToString();
                txtcontact.Text = dr["Student_contact"].ToString();
                txtemail.Text = dr["Student_email"].ToString();
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                i = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update [Library_Management].[dbo].[Student_info] set Student_name='" + txtname.Text + "',Student_address='" + txtadd.Text + "',Student_enrollment_no='" + txtenroll.Text + "',Student_department='" + txtdep.Text + "',Student_sem='" + txtsem.Text + "',Student_contact='" + txtcontact.Text + "',Student_email='" + txtemail.Text + "' where Id=" + i + "";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display_Student();
                
                MessageBox.Show("Updated Successfully");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
