using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using PropertyChanged;

namespace UPD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class MessageShow
    {
        public string message;
        public DateTime time;

        public MessageShow(string message, DateTime time)
        {
            this.message = message;
            this.time = time;
        }

        public string str => $"{message}\t\t\t\t{time.Hour}:{time.Minute}";
    }


    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IpBox.Text), Convert.ToInt32(PortBox.Text));

                IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);

                UdpClient client = new UdpClient();

                string message = MessageBox.Text;
                
                byte[] data = Encoding.Unicode.GetBytes(message);

                client.Send(data, data.Length, ipPoint);

                data = client.Receive(ref remoteIpPoint);

                Encoding.Unicode.GetString(data);

                ChatList.Items.Add(Encoding.Unicode.GetString(data) + "  -  "+ DateTime.Now);
                
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
 }

