using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Lab5_6_Listener
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Listen()
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
                    MessageBox.Show("--> Ожидание соединения");

                    TcpClient client = server.AcceptTcpClient();

                    MessageBox.Show("--> Соединение установлено");

                    data = null;

                    NetworkStream stream = client.GetStream();
                    MessageBox.Show(stream.Length.ToString());
                    int i;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.Default.GetString(bytes, 0, i);

                        MessageBox.Show(data);

                    }

                    client.Close();
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.ToString());
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
