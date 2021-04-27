using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace tcp_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static IPAddress ip = IPAddress.Parse("127.0.0.1");
        private static IPEndPoint iPEndPoint = new IPEndPoint(ip, 56072);
        public MainWindow()
        {
            InitializeComponent();
            inputstr.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!socket.Connected)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(iPEndPoint);
                }
                string data = inputstr.Text;
                byte[] buffer = Encoding.UTF8.GetBytes(data);

                socket.Send(buffer);

                res.Text = inputstr.Text + " 已发送";
            }
            catch (Exception ex)
            {
                res.Text = ex.Message;
            }
            inputstr.Text = "";
            inputstr.Focus();
        }

        public static bool testing = false;
        private static Thread testThread;

        private void threadSend(object sender, RoutedEventArgs e)
        {
            if (testing == false)
            {
                makeTestThread();
            }
            testing = true;
            testThread.Start();
        }

        public void makeTestThread()
        {
            testThread = new Thread(async () =>
            {
                Random rd = new Random();
                while (testing)
                {
                    await Task.Delay(200);

                    if (!socket.Connected)
                    {
                        socket.Connect(iPEndPoint);
                    }

                    var t_value = "";
                    if (is_random)
                    {
                        t_value = rd.Next(1500, 4000).ToString();
                    }
                    else
                    {
                        t_value = "1600";
                    }

                    byte[] buffer = Encoding.UTF8.GetBytes(t_value);

                    socket.Send(buffer);
                }
            });
        }

        private void stopTest(object sender, RoutedEventArgs e)
        {
            testing = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            testing = false;
        }
        private bool is_random = true;
        private void switch_test(object sender, RoutedEventArgs e)
        {
            if (is_random)
            {
                is_random = false;
            }
            else is_random = true;
        }
    }
}
