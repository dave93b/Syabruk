using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Lab9_Sender
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

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Connect("127.0.0.1", 9090);
        }

        public void Connect(string host, int port)
        {
            IPAddress[] IPs = Dns.GetHostAddresses(host);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            InfoTextBlock.Text += String.Format("Establishing Connection to {0}{1}", host, Environment.NewLine);
            s.Connect(IPs[0], port);

            byte[] howdyBytes = Encoding.ASCII.GetBytes(TextToSendTextBox.Text);
            s.Send(howdyBytes);
            byte[] buffer = new byte[50];
            s.Receive(buffer);
            InfoTextBlock.Text += Encoding.ASCII.GetString(buffer) + Environment.NewLine;
            InfoTextBlock.Text += "Connection established" + Environment.NewLine;
        }
    }
}
