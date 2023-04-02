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
    public partial class Register : Form
    {
        int count = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"insert into [Library_Management].[dbo].[Library_Person] values('" + txtfname.Text + "','" + txtuname.Text + "','" + txtpass.Text + "','" + txtemail.Text + "','" + txtcontact.Text + "')";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                count++;
                if (count == 0)
                {
                    MessageBox.Show("Data Not Inserted");
                }
                else
                {
                    MessageBox.Show("Data Inserted Successfully");
                    this.Hide();
                    Login lg = new Login();
                    lg.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
