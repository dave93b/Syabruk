using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_Sender
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
            Console.WriteLine("--> Введите Ваше сообщение и нажмите Enter:");

            while (true)
            {
                byte[] howdyBytes = Encoding.Default.GetBytes(Console.ReadLine());
                s.Send(howdyBytes);
            }
        }
    }
}
