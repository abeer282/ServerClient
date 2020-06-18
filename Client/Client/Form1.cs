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


namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 ff = new Form2();
            this.Hide();
            ff.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 port = int.Parse("13000");
                IPAddress addressserver = IPAddress.Parse("127.0.0.1");
                TcpClient client = new TcpClient(addressserver.ToString(), port);
                byte[] data = System.Text.Encoding.ASCII.GetBytes("9" + textBox1.Text+'$'+textBox2.Text);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                string messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                label3.Text = messagefromserver;
                textBox2.Text = "";
                stream.Close();
                client.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
