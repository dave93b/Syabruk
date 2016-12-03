using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Lab5_6_Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Connect1("127.0.0.1", 9090);

            Console.Read();
        }
        
        public static void Connect1(string host, int port)
        {
            IPAddress[] IPs = Dns.GetHostAddresses(host);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("--> Соединяемся с {0}", host);
            s.Connect(IPs[0], port);
            
            Console.WriteLine("--> Соединение установлено");
            Console.WriteLine("--> Введите путь к файлу и нажмите Enter:");


            var fileBytes = File.ReadAllBytes(Console.ReadLine());

            //while (true)
            //{
            //    byte[] howdyBytes = Encoding.Default.GetBytes(Console.ReadLine());
            //    s.Send(howdyBytes);
            //}
            s.Send(fileBytes);
        }
    }
}
