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
    public partial class Create : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=C:\Users\Maryan\Desktop\ProiectSMP\ProiectSMP\ProiectSMP\Proiect.sdf");
        
        SqlCommand mcd;
        public Create()
        {
            InitializeComponent();
        }

        public void openCon() {
                    if (con.State==ConnectionState.Closed) {
                    con.Open();
                    }
            }

        public void closeCon() {
            if (con.State==ConnectionState.Open) {
                con.Close();
            }
        }
       

        public void executeQuery(string q)
        {
            try
            {
                mcd = new SqlCommand(q,con);
                if (mcd.ExecuteNonQuery() == 1)
                {   
                        MessageBox.Show("Executat cu success");
                 }
                else {
                    MessageBox.Show("Fuck!");
                }
            }
            catch(Exception ex) {
            MessageBox.Show("Alert"+ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.Write(textBox1.Text);
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=ProiectTest;Integrated Security=True");
            
            con.Open();
            MessageBox.Show("1" + con.State);
               
            string s="Insert into Users (username,password,name) values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"')";
            mcd = new SqlCommand(s, con);
            if (mcd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("User-ul a fost creat cu success..Login with your new account!");
                var ss = new Login();
                ss.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Cannot create user!Contact admin! :(");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ss = new Login();
            ss.Show();
        }
    }
}
