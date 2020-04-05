using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SalesTaxCalculator
{
    public class SalesTaxCalculator
    {
        private TcpListener server;
        private byte[] bytes;
        private string data;

        private Logger logger;

        public SalesTaxCalculator()
        {
            int port = 1156;
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(ipAddress, port);

            bytes = new byte[256];
            data = "";
        }

        

        public void Start()
        {
            server.Start();

            while (true)
            {
                logger.Info("Waiting for a connection");

                TcpClient client = server.AcceptTcpClient();
                logger.Debug("Connected");

                NetworkStream stream = client.GetStream();
                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    logger.Info($"Received: {data}");

                    string str = "Thank you!";
                    char[] strCharacters = str.ToCharArray();
                    byte[] response = Encoding.ASCII.GetBytes(strCharacters, 0, strCharacters.Length);

                    stream.Write(response, 0, response.Length);
                    logger.Info($"Sent: {str}");
                }

            }
        }
    }
}
