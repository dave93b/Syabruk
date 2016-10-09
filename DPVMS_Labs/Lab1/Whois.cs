using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Whois
    {
        private const int Whois_Server_Default_PortNumber = 43;
        private const string Domain_Record_Type = "domain";
        private const string DotCom_Whois_Server = "whois.verisign-grs.com";
        
        public static string Lookup(string domainName)
        {
            using (TcpClient whoisClient = new TcpClient())
            {
                whoisClient.Connect(DotCom_Whois_Server, Whois_Server_Default_PortNumber);

                string domainQuery = Domain_Record_Type + " " + domainName + "\r\n";
                byte[] domainQueryBytes = Encoding.ASCII.GetBytes(domainQuery.ToCharArray());

                Stream whoisStream = whoisClient.GetStream();
                whoisStream.Write(domainQueryBytes, 0, domainQueryBytes.Length);

                StreamReader whoisStreamReader = new StreamReader(whoisClient.GetStream(), Encoding.ASCII);

                string streamOutputContent = "";
                List<string> whoisData = new List<string>();
                while (null != (streamOutputContent = whoisStreamReader.ReadLine()))
                {
                    whoisData.Add(streamOutputContent);
                }

                whoisClient.Close();

                return String.Join(Environment.NewLine, whoisData);
            }
        }
    }
}
