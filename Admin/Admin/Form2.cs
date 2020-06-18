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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 ff = new Form3();
            ff.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 port = int.Parse("13000");
                IPAddress addressserver = IPAddress.Parse("127.0.0.1");
                TcpClient client = new TcpClient(addressserver.ToString(), port);
                byte[] data = System.Text.Encoding.ASCII.GetBytes("2"+textBox1.Text);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                string messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                label4.Text = messagefromserver;
                label3.Text = textBox1.Text;
                textBox1.Text = "";
                stream.Close();
                client.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form4 ff = new Form4();
            ff.Show();
            this.Hide();
        }
    }
}
