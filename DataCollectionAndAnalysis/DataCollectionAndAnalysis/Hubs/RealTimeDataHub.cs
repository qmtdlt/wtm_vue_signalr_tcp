using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace DataCollectionAndAnalysis.Hubs
{
    [AuthorizeJwtWithCookie]
    [AllRights]
    public class RealTimeDataHub:Hub
    {
        private static IHubCallerClients _Clients;
        [AllRights]
        public async Task AddPoint(string info)
        {
            _Clients = Clients;
            await _Clients.All.SendAsync("add_point", "链接建立成功");
        }

        /// <summary>
        /// 消息发送线程
        /// </summary>
        /// <param name="clientsocket"></param>
        public static void send_message_thread(Socket clientsocket)
        {
            Thread thread = new Thread(async (obj) =>
            {
                var socket = obj as Socket;
                while (true)
                {
                    try
                    {
                        byte[] buffer = new byte[1024 * 1024];
                        //将接收过来的数据放到buffer中，并返回实际接受数据的长度 
                        int n = socket.Receive(buffer);
                        //将字节转换成字符串 
                        string words = Encoding.UTF8.GetString(buffer, 0, n);
                        if (!string.IsNullOrEmpty(words))
                        {
                            //收到消息，使用praam.clients进行消息发送，不能直接使用Client，Client属于主线程，不能直接使用
                            if (_Clients != null)
                            {
                                await _Clients.All.SendAsync("add_point", words);
                            }                            
                        }
                    }
                    catch (Exception ex)
                    {
                        //连接异常：
                        //异常例1：tcp 客户端关闭了连接，此时终止消息接收线程
                        if (_Clients != null)
                        {
                            await _Clients.All.SendAsync("add_point", ex.Message);
                        }                        
                        break;
                    }
                }
            });
            thread.Start(clientsocket);
        }
    }
}
