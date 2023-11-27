using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    internal class Program
    {
        static List<KeyValuePair<string, List<string>>> m_key = new List<KeyValuePair<string, List<string>>>()
        {
            new KeyValuePair<string, List<string>>("Hello", new List<string>(){"Hi!", "Hey!", "What`s up!"}),
            new KeyValuePair<string, List<string>>("What time is it?", new List<string>(){$"It`s {DateTime.Now}"}),
            new KeyValuePair<string, List<string>>("What date is today", new List<string>(){$"Today its {DateTime.Now.Year + " " + DateTime.Now.Day + " " + DateTime.Now.Month}"}),
        };


        static string address = "127.0.0.1";
        static int port = 8080;             

        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            IPEndPoint remoteEndPoint = null;
            UdpClient listener = new UdpClient(ipPoint);

            try
            {
                Console.WriteLine("Server started! Waiting for connection...");

                while (true)
                {
                    byte[] data = listener.Receive(ref remoteEndPoint);

                    string msg = Encoding.Unicode.GetString(data);

                    if (m_key.Where(x => x.Key == msg).Select(x => x.Key).Count() != 0)
                    {

                        var tmp = m_key.Where(x => x.Key == msg).Select(x => x.Value).ToArray();

                        string a = tmp[new Random().Next(1, tmp.Length)].First();
                        data = Encoding.Unicode.GetBytes(a);
                        listener.Send(data, data.Length, remoteEndPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            listener.Close();
        }

    }
}