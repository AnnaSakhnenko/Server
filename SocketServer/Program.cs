using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Timers;

namespace SocketServer
{
    class Program
    {
        private static System.Timers.Timer aTimer;
        static InfoList IL = new InfoList();
        static int j = 0;

        static void Main(string[] args)
        {
            //InfoList IL = new InfoList();
            Time();
            IL.list = new List<Info>();
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);
               
                while (true)
                {
                    //Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    Socket handler = sListener.Accept();
                    string data = null;
                    
                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    string[] arrInfo = data.Split('|');
                    Info I = new Info();
                    I.email = arrInfo[0] + j + "@test.ru";
                    I.where = arrInfo[1] + "@test.ru";
                    I.text = arrInfo[2];
                    IL.list.Add(I);
                    j++;

                    string reply = "True";
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();                                                       
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }

        static public void Time()
        {
            aTimer = new System.Timers.Timer(3000);
            aTimer.Elapsed += WriteMes;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        static public void WriteMes(Object source, ElapsedEventArgs e)
        {
            if (IL.list.Count != 0)
            {
                Console.Write("Кому: " + IL.list[0].where + "\n");
                Console.Write("Откуда: " + IL.list[0].email + "\n");
                Console.Write("Сообщения: " + IL.list[0].text + "\n\n");
                IL.list.RemoveAt(0);
            }
            //int count = IL.list.Count;
            //for (int i = 0; i < IL.list.Count; i++)
            //{
            //    Console.Write("Кому: " + IL.list[i].where +"\n");
            //    Console.Write("Откуда: " + IL.list[i].email + "\n");
            //    Console.Write("Сообщения: " + IL.list[i].text + "\n\n");
            //    IL.list.RemoveAt(i);                
            //    //j++;
            //} 
        }
    }
}
