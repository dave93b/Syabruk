using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab5_6_Listener
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                Int32 port = 9090;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);

                server.Start();

                Byte[] bytes = new Byte[1000000];
                String data = null;

                while (true)
                {
                    Console.WriteLine("--> Ожидание соединения");

                    TcpClient client = server.AcceptTcpClient();

                    Console.WriteLine("--> Соединение установлено");

                    data = null;

                    NetworkStream stream = client.GetStream();

                    int i;
                    int j = 0;
                    var b=new byte[1000000];
                    while ((i = stream.ReadByte()) != -1)
                    {
                        //data = Encoding.Default.GetString(bytes, 0, i);
                        //Console.ForegroundColor = ConsoleColor.Yellow;
                        //Console.WriteLine("--> Получено сообщение: {0}", data);
                        //Console.ForegroundColor = ConsoleColor.White;
                        b[j++] = Convert.ToByte(i);
                    }
                    File.WriteAllBytes("D:\\1", b);

                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();

        }
    }
}
