using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//libraries2use
using System.IO;//input output
using System.Net.Sockets;//for TCP-IP conncetion
using System.Net;
using System.Data.OleDb;//Microsoft Access Database


namespace Server
{
    class Program
    {

        public static void AdminLogs(string newLog)
        {
            FileStream f = new FileStream("AdminLogs.txt", FileMode.Append);
            StreamWriter s = new StreamWriter(f);
            s.WriteLine(newLog+" Time: "+ DateTime.Now.ToShortTimeString());
            s.Close();
            f.Close();
        }
        public static void UserLogs(string newLog)
        {
            FileStream f = new FileStream("UserLogs.txt", FileMode.Append);
            StreamWriter s = new StreamWriter(f);
            s.WriteLine(newLog + " Time: " + DateTime.Now.ToShortTimeString());
            s.Close();
            f.Close();
        }
        public static string addQuestion(string str)
        {
            try
            {
                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=myDatabase.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                str = str.Substring(1);
                AdminLogs("Admin Added Question - details: "+str);/*****************************Log******************************/
                int indexx = str.IndexOf('#');
                string question = str.Substring(0, indexx);
                str = str.Substring(indexx+1);
                indexx = str.IndexOf('#');
                string choice1 = str.Substring(0, indexx);
                str = str.Substring(indexx + 1);
                indexx = str.IndexOf('#');
                string choice2 = str.Substring(0, indexx);
                str = str.Substring(indexx + 1);
                indexx = str.IndexOf('#');
                string choice3 = str.Substring(0, indexx);
                str = str.Substring(indexx + 1);
                indexx = str.IndexOf('#');
                string choice4 = str.Substring(0, indexx);
                int answer = int.Parse(str.Substring(str.IndexOf('#') + 1));

                cmd.CommandText = "insert into questions (Question, choice1,choice2,choice3,choice4,trueAnswer) values ('" + question + "','" + choice1 + "','"+choice2 + "','" + choice3 + "','" + choice4 + "'," + answer + ");";
                int n = cmd.ExecuteNonQuery();
                conn.Close();
                return ("insert " + n.ToString() + " row");

            }
            catch (Exception err)
            {
                return (err.Message);
            }
        }
        public static string updateNewUser(string str)
        {
            try
            {
                
                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=myDatabase.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                string[] splitty = (str+'$').Split('$');
                
                //0- id  1-name  2-pswd   3-grade
                cmd.CommandText = "insert into users (user_id, user_name,password1,grade) values ('" + splitty[0] + "','" + splitty[1] + "','" + splitty[2] + "'," + int.Parse(splitty[3]) + ");";
                int n = cmd.ExecuteNonQuery();
                
                conn.Close();
                UserLogs("New user was Added!");/*****************************Log******************************/
                return "ok";
               
            }
            catch (Exception err)
            {
                return (err.Message);
            }
        }
        public static string getGradeByID(string str)
        {
            try
            {
                string strDb;
                str = str.Substring(1);
                Console.WriteLine(str);
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=myDatabase.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbDataReader dr;

                OleDbCommand cmd = new OleDbCommand("Select grade from users where user_name='" + str + "';", conn);
                dr = cmd.ExecuteReader();
                string str_new = "";
                while (dr.Read())
                    str_new += dr[0].ToString();
                    
                dr.Close();
                conn.Close();

                if (str_new.Length == 0)
                    str_new = "User Not Found!";
                AdminLogs("Admin Asked about the grade of : " + str+" Result : "+str_new);/******************************Log*****************************/
                return str_new;

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return "Error";
            }
        }
        public static string userGetGrade(string str)
        {
            try
            {
                string strDb;
                
                Console.WriteLine(str);
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=myDatabase.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbDataReader dr;
                string[] splitty = str.Split('$');
                
                OleDbCommand cmd = new OleDbCommand("Select password1 from users where user_id='" + splitty[0] + "';", conn);
                dr = cmd.ExecuteReader();
                string str_new = "";
                while (dr.Read())
                    str_new += dr[0].ToString();
               

                dr.Close();
                conn.Close();


                if (str_new != splitty[1])
                    str_new = "Wrong Passowrd, Try Again!";
                else
                {
                    string strDb1;

                    
                    strDb1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=myDatabase.accdb;" + "Persist Security Info=False";
                    OleDbConnection conn1 = new OleDbConnection(strDb1);
                    conn1.Open();
                    OleDbDataReader dr1;
                    
                    OleDbCommand cmd1 = new OleDbCommand("Select user_name,grade from users where user_id='" + splitty[0] + "';", conn1);
                    dr1 = cmd1.ExecuteReader();
                    str_new = "";
                    while (dr1.Read())
                        str_new ="Name: " +dr1[0].ToString()+"\n"+"Grade: "+ dr1[1].ToString();
                    
                    dr1.Close();
                    conn1.Close();
                }
                UserLogs("User Asked about the grade of id: " + splitty[0] + " Result : " + str_new);/******************************Log*****************************/
                return str_new;

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return "Error";
            }
        }
        public static string getQuestion(int questionNumber)
        {
            try
            {
                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=myDatabase.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbDataReader dr;
                OleDbCommand cmd = new OleDbCommand("Select * from questions where number=" +questionNumber+ ";", conn);
                dr = cmd.ExecuteReader();
                string str_new = "";
                while (dr.Read())
                    str_new += dr[0].ToString()+"$"+dr[1].ToString()+ "$" + dr[2].ToString()+ "$" + dr[3].ToString()+ "$" + dr[4].ToString()+"$"+ dr[5].ToString() + "$";

                dr.Close();
                conn.Close();

                if (str_new.Length == 0)
                    str_new = "End of Data!";
                
                
                return str_new;

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return "Error";
            }
        }
        public static string removeQuestion(string questionText)
        {
            try
            {
                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=myDatabase.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbDataReader dr;
                OleDbCommand cmd = new OleDbCommand("delete from questions where Question='" + questionText + "';", conn);
                dr = cmd.ExecuteReader();
                dr.Close();
                conn.Close();

                AdminLogs("Admin Deleted a Question - details: " + questionText);/*****************************Log******************************/
                return "Question Deleted";
                 

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return "Error";
            }
        }
        public static void createAdmin()
        {
            FileStream f = new FileStream("pswd.txt", FileMode.Create);
            BinaryWriter sr = new BinaryWriter(f);
            sr.Write("superadmin");
            sr.Write(123456);
            sr.Write(100);
            sr.Close();
            f.Close();
        }
        public static string getAllQuestions()
        {
            string ans = "";
            try
            {
                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=myDatabase.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbDataReader dr;
                OleDbCommand cmd = new OleDbCommand("Select * from questions ;", conn);
                dr = cmd.ExecuteReader();
                  
                while (dr.Read())
                    ans += dr[0].ToString() + "$";
                AdminLogs("Admin shows all questions");/******************************Log*****************************/
                dr.Close();
                conn.Close();

                return ans;

            }catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return "Error";
            }
        }
        public static string howManyQuestions()
        {
            int ans = 0;
            try
            {
                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=myDatabase.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbDataReader dr;
                OleDbCommand cmd = new OleDbCommand("Select * from questions ;", conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                    ans++;
                dr.Close();
                conn.Close();

                return ans.ToString();

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return "Error";
            }
        }
        public static string getAdminCredintials()
        {

            FileStream f = new FileStream("pswd.txt", FileMode.Open);
            BinaryReader sr = new BinaryReader(f);
            string str = sr.ReadString()+'$'+ sr.ReadInt32();
            Console.WriteLine(str);
            sr.Close();
            f.Close();
            AdminLogs("Admin trying to enter"); /******************************Log*****************************/
            return str;
        }
        public static string WhoisConnecting(string str)
        {
            
            string str_new="";
            if (str[0] == '1')
                str_new = addQuestion(str);
            else if (str[0] == '2')
                str_new = getGradeByID(str);
            else if (str[0] == '3')
                str_new = getQuestion(int.Parse(str.Substring(1)));
            else if (str[0] == '0')
                createAdmin();
            else if (str[0] == '5')
                str_new = getAllQuestions();
            else if (str[0] == '6')
                str_new = getAdminCredintials();
            else if (str[0] == '7')
                str_new = removeQuestion(str.Substring(1));
            else if (str[0] == '8')
                str_new = howManyQuestions();
            else if(str[0]=='9')
                str_new = userGetGrade(str.Substring(1));
            else if (str[0] == '4')
                str_new = updateNewUser(str.Substring(1));

            return str_new;
        }


        static void Main(string[] args)
        {
            
            try
            {
                //initializing ip and port for connections later
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                TcpListener server = new TcpListener(localAddr, port);
           
                server.Start();

                Console.WriteLine("Server Address: " + server.LocalEndpoint.ToString());

                byte[] bytes = new byte[256];
                string data = null;

                while (true)
                {
                    Console.WriteLine("Waiting to connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connecting ...");

                    Console.WriteLine(client.Client.RemoteEndPoint.ToString());
                    Console.WriteLine("Time is : {0}", DateTime.Now.ToShortTimeString());
                    data = null;
                    NetworkStream stream = client.GetStream();
                    int i;

                    i = stream.Read(bytes, 0, bytes.Length);
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    //Console.WriteLine("Message from client : {0}", data);

                    string data1 = WhoisConnecting(data);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data1);
                    stream.Write(msg, 0, msg.Length);
                    
                    client.Close();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            Console.Read();

        }

    }
}
