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
    public partial class Add_Student_info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        public Add_Student_info()
        {
            InitializeComponent();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into [Library_Management].[dbo].[Student_info] values('" + txtname.Text + "','" + txtaddress.Text + "','" + txtenroll.Text + "','" + txtdep.Text + "','" + txtsem.Text + "','" + txtcontact.Text + "','" + txtemail.Text + "')";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Student_info inserted sucessfully");
                txtname.Text = txtaddress.Text = txtenroll.Text = txtdep.Text = txtsem.Text = txtcontact.Text = txtemail.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
