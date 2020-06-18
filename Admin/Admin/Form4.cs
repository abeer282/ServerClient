using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;//input output
using System.Net.Sockets;//for TCP-IP conncetion
using System.Net;
using System.Data.OleDb;//Microsoft Access Database
namespace Admin
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                Int32 port = int.Parse("13000");
                IPAddress addressserver = IPAddress.Parse("127.0.0.1");
                TcpClient client = new TcpClient(addressserver.ToString(), port);

                byte[] data = System.Text.Encoding.ASCII.GetBytes("5");
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                string messagefromserver = "";
                data = new byte[1024];
                int bytes = stream.Read(data, 0, data.Length);
                messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                string[] splitty = messagefromserver.Split('$');
               
                for (int i=0;i<splitty.Length;i++) {
                    
                    listBox1.Items.Add(splitty[i]);
                }
                stream.Close();
                client.Close();
               
            }
           
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 port = int.Parse("13000");
                IPAddress addressserver = IPAddress.Parse("127.0.0.1");
                TcpClient client = new TcpClient(addressserver.ToString(), port);

                byte[] data = System.Text.Encoding.ASCII.GetBytes("7"+ listBox1.GetItemText(listBox1.SelectedItem));
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                string messagefromserver = "";
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                if (messagefromserver=="Question Deleted")
                {
                    MessageBox.Show("Question Deleted!");
                }
                stream.Close();
                client.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 ff = new Form2();
            this.Hide();
            ff.Show();
        }
    }
}
