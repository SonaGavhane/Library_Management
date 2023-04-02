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
    public partial class Add_Books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        public Add_Books()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into [Library_Management].[dbo].[Books_info] values('" + txtbookname.Text + "','" + txtauthorname.Text + "','" + txtpupname.Text + "','" + dateTimePicker1.Text + "'," + txtprice.Text + "," + txtquantity.Text + ","+txtquantity.Text+")";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                txtbookname.Text = txtauthorname.Text = txtpupname.Text = " ";
                txtprice.Text = "";
                txtquantity.Text = "";
                MessageBox.Show("Books Added Successfully!!!");
                this.Hide();
                mdi_user mu = new mdi_user();
                mu.Show();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Add_Books_Load(object sender, EventArgs e)
        {

        }
    }
}
