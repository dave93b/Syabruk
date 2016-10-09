using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.NetworkInformation;

namespace Lab3
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            
            options.DontFragment = true;
            
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            try
            {
                PingReply reply = pingSender.Send(textBox.Text, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    textBoxForResult.Text = String.Format("Хост: {0}\n", reply.Address.ToString());
                    textBoxForResult.Text += String.Format("Время: {0} мсек\n", reply.RoundtripTime);
                    textBoxForResult.Text += String.Format("Время жизни пакета (TTL): {0}\n", reply.Options.Ttl);
                    textBoxForResult.Text += String.Format("Число байт: {0}\n", reply.Buffer.Length);
                }
            }
            catch (Exception ex)
            {
                textBoxForResult.Text = ex.Message;
            }


        }
    }
}
