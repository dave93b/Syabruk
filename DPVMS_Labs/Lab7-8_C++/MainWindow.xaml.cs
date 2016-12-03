using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace Lab7_8_C__
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

        public IEnumerable<TracertEntry> Tracert(string ipAddress, int maxHops, int timeout)
        {
            IPAddress address;
            
            if (!IPAddress.TryParse(ipAddress, out address))
                throw new ArgumentException(string.Format("{0} is not a valid IP address.", ipAddress));
            
            if (maxHops < 1)
                throw new ArgumentException("Max hops can't be lower than 1.");
            
            if (timeout < 1)
                throw new ArgumentException("Timeout value must be higher than 0.");


            Ping ping = new Ping();
            PingOptions pingOptions = new PingOptions(1, true);
            Stopwatch pingReplyTime = new Stopwatch();
            PingReply reply;

            do
            {
                pingReplyTime.Start();
                reply = ping.Send(address, timeout, new byte[] { 0 }, pingOptions);
                pingReplyTime.Stop();

                string hostname = string.Empty;
                if (reply.Address != null)
                {
                    try
                    {
                        hostname = Dns.GetHostEntry(reply.Address).HostName;
                    }
                    catch (SocketException) { /* No host available for that address. */ }
                }
                
                yield return new TracertEntry()
                {
                    HopID = pingOptions.Ttl,
                    Address = reply.Address == null ? "N/A" : reply.Address.ToString(),
                    Hostname = hostname,
                    ReplyTime = pingReplyTime.ElapsedMilliseconds,
                    ReplyStatus = reply.Status
                };

                pingOptions.Ttl++;
                pingReplyTime.Reset();
            }
            while (reply.Status != IPStatus.Success && pingOptions.Ttl <= maxHops);
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            //Task<IEnumerable<TracertEntry>> tasks=Task<IEnumerable<TracertEntry>>.Factory.StartNew(()=> Tracert(Dns.GetHostAddresses(textBox.Text)[0].ToString(), 30, 5000));
            //var result = Task.Run(() => Tracert(Dns.GetHostAddresses(textBox.Text)[0].ToString(), 30, 5000));
            //tasks.Wait();
            foreach (var entry in Tracert(Dns.GetHostAddresses(textBox.Text)[0].ToString(), 30, 5000))
            {
                textBoxForResult.Text+=entry+Environment.NewLine;
            }
        }
    }

    public class TracertEntry
    {
        public int HopID { get; set; }

        public string Address { get; set; }
        
        public string Hostname { get; set; }
        
        public long ReplyTime { get; set; }

        public IPStatus ReplyStatus { get; set; }

        public override string ToString()
        {
            return string.Format("{0,-5} {1,-70} {2}",
                HopID,
                string.IsNullOrEmpty(Hostname) ? Address : Hostname + "[" + Address + "]",
                ReplyStatus == IPStatus.TimedOut ? "Request Timed Out." : ReplyTime.ToString() + " ms"
                );
        }
    }
}
