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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 ff = new Form2();
            this.Hide();
            ff.Show();
        }
        private int radioChecked()
        {
            if (radioButton1.Checked)
                return 1;
            else if(radioButton2.Checked)
                return 2;
            else if (radioButton3.Checked)
                return 3;
            return 4;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                string message;
                Int32 port = int.Parse("13000");
                IPAddress addressserver = IPAddress.Parse("127.0.0.1");
                TcpClient client = new TcpClient(addressserver.ToString(), port);
                int radiochecked = radioChecked();
                message = "1" + textBox1.Text + "#" + textBox2.Text + "#" + textBox3.Text + "#" + textBox4.Text+ "#" + textBox5.Text + "#" + radiochecked;


                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                string messagefromserver = "";
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                MessageBox.Show("Answer from Server :" + messagefromserver);
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
