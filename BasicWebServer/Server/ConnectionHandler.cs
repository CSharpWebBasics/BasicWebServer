﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BasicWebServer.Server.Handlers;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Routing.Contracts;


namespace BasicWebServer.Server
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync()
        {
            string requestString = this.ReadRequest().Result;

            var httpRequest = new HttpRequest(requestString);

            var httpResponse = new HttpHandler(this.serverRouteConfig).Handle(httpRequest);

            var responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());

            var byteSegment = new ArraySegment<byte>(responseBytes);

            await this.client.SendAsync(byteSegment, SocketFlags.None);

            Console.WriteLine("--Request-----");
            Console.WriteLine(requestString);
            Console.WriteLine("--Response-----");
            Console.WriteLine(httpResponse);
            Console.WriteLine();

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<string> ReadRequest()
        {
            string request = string.Empty;

            ArraySegment<byte> data = new ArraySegment<byte>(new byte[1024]);

            int numBytesRead;

            while ((numBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None)) > 0)
            {
                request += Encoding.UTF8.GetString(data.Array, 0, numBytesRead);
                if (numBytesRead < 1024)
                {
                    break;
                }
            }

            return request;
        }
    }
}
