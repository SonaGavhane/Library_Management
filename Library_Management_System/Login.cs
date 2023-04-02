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
    public partial class Login : Form
    {
        int count = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        public Login()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from [Library_Management].[dbo].[Library_Person] where UserName='" + txtuser.Text + "' and Password='" + txtpass.Text + "'";
                con.Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = int.Parse(dt.Rows.Count.ToString());
                con.Close();
                if (count == 0)
                {
                    MessageBox.Show("UserName and Password doesn't match");
                    txtuser.Text = txtpass.Text = " ";
                }
                else
                {
                    this.Hide();
                    mdi_user mu = new mdi_user();
                    mu.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtuser.Text = txtpass.Text = "";
        }

        private void btnnewuser_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            this.Hide();
            reg.Show();
        }

       
    }
}
