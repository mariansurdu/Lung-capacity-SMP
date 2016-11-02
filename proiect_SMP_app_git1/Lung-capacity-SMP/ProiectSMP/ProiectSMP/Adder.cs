using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Text;
namespace ProiectSMP
{
    public partial class Adder : Form
    {
        string value =""+0.0;
        string lastValue=""+0.0;
        int idUser;
        float LastValue = 0;
        
        private Timer timer1;
        public Adder()
        {
            InitializeComponent();

        }

        private void Adder_Load(object sender, EventArgs e)
        {

        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 500; // in miliseconds
            timer1.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
             {   //MessageBox.Show("executed!");
                 StartReceiveThread();
                 textBox1.Text = value;
             }

       

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=ProiectTest;Integrated Security=True");
            SqlDataAdapter sda1 = new SqlDataAdapter("Select Top 1  * from sessions ", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            string idUser = dt1.Rows[0][0].ToString();
            
            //save Data to database
            var date = DateTime.Now;
            //MessageBox.Show(""+date);
            
            con.Open();
           // MessageBox.Show("1" + con.State);
          float xvalue = float.Parse(value);
            string s = "Insert into Data (idUser,date,value) values('" + idUser + "','" +date+ "','" + xvalue  + "')";
           var mcd = new SqlCommand(s, con);
            if (mcd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Datele au fost adaugate cu success!!");
                var ss = new Main();
                ss.Show();
                this.Hide();
            }
            else
            {
                  MessageBox.Show("Cannot save data!Contact admin! :(");
            }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ss = new Main();
            this.Hide();
            ss.Show();
        }
        public void StartReceiveThread()
        {
            //MessageBox.Show("Start thread");
            var thd = new System.Threading.Thread(receiveThreadFunc);
            thd.Start();
        }
        public void DataReceived(string data)
        {
            // do something with the data here
            //MessageBox.Show(data);
            
           
                value = data;
           
        }



        public void receiveThreadFunc()
        {
            using (var serial1 = new System.IO.Ports.SerialPort("COM4", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One))
            {
                // open serial port
                if (!serial1.IsOpen)
                {
                    try { serial1.Open(); }
                    catch (Exception ex) { 
                    MessageBox.Show("Port was closed");
                    }
                }
                // send init command
                var initCommand = new byte[5];
                serial1.Write(initCommand, 0, initCommand.Length);

                // get start time
                DateTime start = DateTime.Now;
                // buffer for pushing received string data into
                StringBuilder indata = new StringBuilder();

                // loop until at most 10 seconds have passed 
                while ((DateTime.Now - start).TotalSeconds < 2)
                {
                    if (serial1.BytesToRead > 0)
                    {
                        // allocate a buffer, up to 1K in length, to receive into
                        int blen = Math.Min(1024, serial1.BytesToRead);
                        byte[] buffer = new byte[blen];
                        // read chunks of data until none left
                        while (serial1.BytesToRead > 0)
                        {
                            //MessageBox.Show(">0");
                            int rc = serial1.Read(buffer, 0, blen);
                            // convert data from ASCII format to string and append to input buffer
                            indata.Append(Encoding.ASCII.GetString(buffer, 0, rc));
                        }
                    }
                    else
                        System.Threading.Thread.Sleep(2);

                    // check for EOL
                    if (indata.Length > 0 && indata.ToString().EndsWith("\r\n"))
                        break;
                }

                if (indata.Length > 0)
                {
                    // post data to main thread, via Invoke if necessary:
                    string data =indata[indata.Length - 6].ToString()+indata[indata.Length - 5].ToString()+indata[indata.Length - 4].ToString()+ indata[indata.Length - 3].ToString() + indata[indata.Length - 2].ToString()+indata[indata.Length-1].ToString();
                   // MessageBox.Show(indata.ToString());
                   // MessageBox.Show("d"+data);
                  
                    if (this.InvokeRequired)
                        this.Invoke(new Action(() => { DataReceived(data); }));
                    else
                        this.DataReceived(data);
                }
            }
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            InitTimer();
            
            textBox1.Text = value ;
           
            
            /*  myport.BaudRate = 9600;
              myport.PortName = "COM4";
              myport.Open();
              while (true)
              {
                  string data_rx = myport.ReadLine();
                  textBox1.Text = data_rx.ToString();
              } */
        }

        private void button6_Click(object sender, EventArgs e)
        {
           SerialPort myport = new SerialPort();
           myport.BaudRate = 9600;
           myport.PortName = "COM4";
           myport.Open();
            if (myport.IsOpen) 
            {
                MessageBox.Show("aa");
                myport.DtrEnable=false; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var serial1 = new System.IO.Ports.SerialPort("COM4", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One))
            {
                // open serial port
                if (!serial1.IsOpen)
                {
                    try { serial1.Open(); }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Port was closed");
                    }
                }
                


                serial1.Write(new byte[] { 0xE0, 0xE1, 0xE2 }, 0, 3);
                      }
    


            





        }
    }
}
