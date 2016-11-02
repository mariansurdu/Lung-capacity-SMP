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
using System.Globalization;

namespace ProiectSMP
{
    public partial class Main : Form
    {
        DateTime date;
        int idUserSession;
        public Main()
        {
            InitializeComponent();
            dataGridView2.Visible = false;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=ProiectTest;Integrated Security=True");
            SqlDataAdapter sda1 = new SqlDataAdapter("Select Top 1  * from sessions ", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            string name = dt1.Rows[0][1].ToString();
            string id = dt1.Rows[0][0].ToString();

            idUserSession = Int32.Parse(id);
            MessageBox.Show(""+idUserSession);
            //idUser session set
            label2.Text =name;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            date = monthCalendar1.SelectionRange.Start;
            DateTime date1 = monthCalendar1.SelectionRange.Start;
            MessageBox.Show("" + date1.Date+date1.Day+date1.Year+date1.Month);
            date = new DateTime(date1.Year,date1.Month,date1.Day);
           // String test = date.ToString("dd.MM.yyy");
            String test = date.ToString("MM/dd/yyyy");
            DateTime dt = DateTime.ParseExact(test, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            date = dt;

            MessageBox.Show("1"+date);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ss = new Adder();
            ss.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=ProiectTest;Integrated Security=True");
            con.Open();
            string s = "Delete from sessions";
            var mcd = new SqlCommand(s, con);
            if (mcd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Logout cu success!");
                var ss = new Login();
                ss.Show();
                this.Hide();
            }
            else
            {
                //MessageBox.Show("Cannot logout!..Contact admin!");
                var ss = new Login();
                ss.Show();
                this.Hide();
            }

           }

        private void Main_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'proiectTestDataSet.Data' table. You can move, or remove it, as needed.
            this.dataTableAdapter.Fill(this.proiectTestDataSet.Data);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //select data from db
            MessageBox.Show("" + idUserSession + date);
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=ProiectTest;Integrated Security=True");

            SqlDataAdapter sda1 = new SqlDataAdapter("Select  * from Data where idUser='" + idUserSession + "'and date='" + date + "'", con); //and date='"+date+"'
            DataTable dt1 = new DataTable();
            MessageBox.Show("Select  * Data where idUser='" + idUserSession + "' and date='" + date + "'");
            sda1.Fill(dt1);
            //string name = dt1.Rows[0][1].ToString();
            //MessageBox.Show(name);
            dataGridView2.Visible = true;
            dataGridView2.DataSource = dt1;
            
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
