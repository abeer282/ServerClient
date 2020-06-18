using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class Exam
    {
        private int questionNumber;
        private string question;
        private string choice1;
        private string choice2;
        private string choice3;
        private string choice4;
        private int trueAnswer;
        public Exam() { }


        public string getQuestionFromServer(int questionNumber,string portt,string ip)
        {
            try
            {

                string message;
                Int32 port = int.Parse(portt);
                IPAddress addressserver = IPAddress.Parse(ip);
                TcpClient client = new TcpClient(addressserver.ToString(), port);
                message = "3" + questionNumber;

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                string messagefromserver = "";
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);


                this.questionNumber = questionNumber;
                string [] mySplit = messagefromserver.Split('$');
                this.question=mySplit[0];
                this.choice1 = mySplit[1];
                this.choice2 = mySplit[2];
                this.choice3 = mySplit[3];
                this.choice4 = mySplit[4];
                this.trueAnswer = int.Parse(mySplit[5]);



                stream.Close();
                client.Close();
                return messagefromserver;

            }
            catch (Exception err)
            {
                return "Please contact the admin with the following error:\n"+err.Message;
            }
            


        }




        public string getQuestion()
        {
            return question;
        }
        public string getChoice1()
        {
            return choice1;
        }
        public string getChoice2()
        {
            return choice2;
        }
        public string getChoice3()
        {
            return choice3;
        }
        public string getChoice4()
        {
            return choice4;
        }
        public int getTrueAnswer()
        {
            return trueAnswer;
        }
    }
}
