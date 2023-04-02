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
    public partial class Return_book : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        public Return_book()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fill_grid();
            panel3.Visible = true;
        }

        private void Return_book_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

        }
        public void Fill_grid()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select *from [Library_Management].[dbo].[Issue_Book] where Student_Enrollment_no='"+txtenroll.Text+"' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            int i;
            i = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select *from [Library_Management].[dbo].[Issue_Book] where Id="+i+"";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                lblbname.Text = dr["Books_name"].ToString();
                lblissuedate.Text = dr["Books_issue_date"].ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            i = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Update [Library_Management].[dbo].[Issue_Book] set Books_return_date='"+dateTimePicker1.Value.ToShortDateString()+"'   where Id=" + i + "";
            cmd.ExecuteNonQuery();

            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "update [Library_Management].[dbo].[Books_info] set Available_quantity=Available_quantity+1 where Books_name='"+lblbname.Text+"'";
            cmd1.ExecuteNonQuery();
            MessageBox.Show("Books updated Successfully");
            
            panel2.Visible = true;
            Fill_grid();
        }
    }
}
