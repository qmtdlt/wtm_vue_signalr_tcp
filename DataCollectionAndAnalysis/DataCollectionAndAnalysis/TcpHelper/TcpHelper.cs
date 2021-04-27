using DataCollectionAndAnalysis.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataCollectionAndAnalysis.TcpHelper
{
    public class TcpHelper
    {
        public static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static IPAddress ip = IPAddress.Parse("127.0.0.1");
        private static IPEndPoint iPEndPoint = new IPEndPoint(ip, 56072);
        
        /// <summary>
        /// tcp 绑定，启动客户端连接监听
        /// 客户端连接后，启动消息发送线程
        /// </summary>
        public static void start_bind_listen()
        {
            try
            {
                socket.Bind(iPEndPoint);
                socket.Listen(10);
                //当页面建立链接，启动线程
                new Thread((obj) => {
                    var tsocket = obj as Socket;
                    while (true)
                    {
                        //第一个线程死循环，监听客户端链接
                        Socket client = tsocket.Accept();
                        //当监听到客户端链接，启动消息接受线程，接受该客户端的消息
                        RealTimeDataHub.send_message_thread(client);
                    }

                }).Start(socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
