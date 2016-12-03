using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_Listener
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
                
                Byte[] bytes = new Byte[256];
                String data = null;
                
                while (true)
                {
                    Console.WriteLine("--> Ожидание соединения");
                    
                    TcpClient client = server.AcceptTcpClient();
                    
                    Console.WriteLine("--> Соединение установлено");

                    data = null;
                    
                    NetworkStream stream = client.GetStream();

                    int i;
 
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.Default.GetString(bytes, 0, i);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("--> Получено сообщение: {0}", data);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
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
