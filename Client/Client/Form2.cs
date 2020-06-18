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
    public partial class Form2 : Form
    {
        int trueAnswer;
        int num = 0;
        int i = 0;
        int grade = 0;
        string user;
        public Form2()
        {
            InitializeComponent();
           
        }

        private static int howMany()
        {
            
            try
            {
                int ans = 0;
                string message;
                Int32 port = int.Parse("13000");
                IPAddress addressserver = IPAddress.Parse("127.0.0.1");
                TcpClient client = new TcpClient(addressserver.ToString(), port);
                message = "8" ;

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                string messagefromserver = "";
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                ans = int.Parse(messagefromserver);
                stream.Close();
                client.Close();
                return ans;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return 0;
            }

            
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            if (i > 10)
            {
                try
                {
                    user += ("$" + grade);
                    Int32 port = int.Parse("13000");
                    IPAddress addressserver = IPAddress.Parse("127.0.0.1");
                    TcpClient client = new TcpClient(addressserver.ToString(), port);
                    

                    byte[] data = System.Text.Encoding.ASCII.GetBytes(user);
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);

                    string messagefromserver = "";
                    data = new byte[256];
                    int bytes = stream.Read(data, 0, data.Length);
                    messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    if (messagefromserver == "ok")
                    {
                        MessageBox.Show("Grade= " + grade + "\nYour Data has been updated in the Database!");
                        Form1 ff = new Form1();
                        this.Hide();
                        ff.Show();
                    }
                    else
                    {
                        MessageBox.Show(messagefromserver);
                    }
                    stream.Close();
                    client.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }


            num = howMany();
            if (num <10)
            {
                MessageBox.Show("Questions are not enough for doing exam.\nContact Admin!");
                Form1 ff = new Form1();
                this.Hide();
                ff.Show();
            }
            try
             {
                 Exam myexam = new Exam();
                 myexam.getQuestionFromServer(i,"13000","127.0.0.1");
                 label1.Text = myexam.getQuestion();
                 radioButton1.Text = myexam.getChoice1();
                 radioButton2.Text = myexam.getChoice2();
                 radioButton3.Text = myexam.getChoice3();
                 radioButton4.Text = myexam.getChoice4();
                 trueAnswer = myexam.getTrueAnswer();
                 radioButton1.Checked = false;
                 radioButton2.Checked = false;
                 radioButton3.Checked = false;
                 radioButton4.Checked = false;
                 i++;
             
             }
             catch (Exception err)
             {
                 MessageBox.Show(err.Message);
                
             }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (trueAnswer==1)
                grade++;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (trueAnswer == 2)
                grade++;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (trueAnswer == 3)
                grade++;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (trueAnswer == 4)
                grade++;
        }

        private Boolean checkId(string str)
        {
            if (str.Length != 9)
                return false;

            for (int j=0;j<str.Length;j++)
            {
                if (str[j]<'0'|| str[j]>'9')
                    return false;
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text==textBox4.Text&&checkId(textBox2.Text)&& textBox3.Text.Length>5)
            {
                user = "4"+textBox2.Text + '$' + textBox1.Text + '$' + textBox3.Text;
                label2.Hide();
                label3.Hide();
                label4.Hide();
                label5.Hide();
                label6.Hide();
                button2.Hide();
                textBox1.Hide();
                textBox2.Hide();
                textBox3.Hide();
                textBox4.Hide();
                label1.Show();
                radioButton1.Show();
                radioButton2.Show();
                radioButton3.Show();
                radioButton4.Show();
                button1.Show();
            }
            else{
                if (!checkId(textBox2.Text)) {
                    MessageBox.Show("Check id, must be 9 digits");
                }
                else
                {
                    MessageBox.Show("Password failed!\nPassword must be more than 6 characters and password confirmation must match passowrd!\nTry again.");
                }
            }
        }
    }
}
