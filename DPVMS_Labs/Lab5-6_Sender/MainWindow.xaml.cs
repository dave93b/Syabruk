using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace Lab5_6_Sender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _filePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog=new OpenFileDialog();
            if (fileDialog.ShowDialog()!=null)
            {
                _filePath=fileDialog.FileName;
                LoadFileTextBox.Text = _filePath;
            }
        }

        public void SendFile(string ipAddress, Int32 port)
        {
            IPAddress[] IPs = Dns.GetHostAddresses(ipAddress);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            s.Connect(IPs[0], port);
            
            byte[] fileBytes = File.ReadAllBytes(_filePath);
            s.Send(fileBytes);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendFile(IpAddressTextBox.Text, Convert.ToInt32(PortTextBox.Text));
        }
    }
}
