using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ProiectSMP
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=ProiectTest;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Users where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("It's time to set session");
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * from Users where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                string idUser = dt1.Rows[0][0].ToString();
                var date = new DateTime();
                con.Open();
                string s = "Insert into sessions (idUser,name) values('" + idUser + "','" + textBox1.Text+ "')";
                var mcd = new SqlCommand(s, con);
                if (mcd.ExecuteNonQuery() == 1)
                {
                   // MessageBox.Show("Sessiunea a fost creata cu success!");
                    
                }
                else
                {
                    MessageBox.Show("Cannot create session!Contact admin! :(");
                } 



                this.Hide();
                Main ss1 = new Main();
                ss1.Show();
            }
            else
            {
                MessageBox.Show("Please check your username and password");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Create ss = new Create();
            ss.Show();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
        
        }
    }
}
