using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usr;
            string pswd;
            try
            {


                Int32 port = int.Parse("13000");
                IPAddress addressserver = IPAddress.Parse("127.0.0.1");
                TcpClient client = new TcpClient(addressserver.ToString(), port);
                byte[] data = System.Text.Encoding.ASCII.GetBytes("6");
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                string messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                string[] splitty = messagefromserver.Split('$');
                usr = splitty[0];
                pswd =splitty[1];
                stream.Close();
                client.Close();
                if (textBox1.Text == usr && textBox2.Text == pswd)
                {
                    Form2 ff = new Form2();
                    this.Hide();
                    ff.Show();
                }
                else
                {
                    MessageBox.Show("Wrong input, please try again!");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click_1(object sender, EventArgs e)
        {/*
            *
            * This Part for button 2 for creating a new admin cridentials
            * 
           /* try
            {


                Int32 port = int.Parse("13000");
                IPAddress addressserver = IPAddress.Parse("127.0.0.1");
                TcpClient client = new TcpClient(addressserver.ToString(), port);
                byte[] data = System.Text.Encoding.ASCII.GetBytes("0");
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                string messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                stream.Close();
                client.Close();
                button2.Hide();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }*/
        }
    }
}
