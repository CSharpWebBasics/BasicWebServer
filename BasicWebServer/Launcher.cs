using BasicWebServer.Application;
using BasicWebServer.Server;
using BasicWebServer.Server.Routing;
using BasicWebServer.Server.Routing.Contracts;
using System;

namespace BasicWebServer
{
    public class Launcher
    {
        public static void Main()
        {
            int Port = 1337;

            IAppRouteConfig routeConfig = new AppRouteConfig();

            var app = new MainApplication();

            app.Configure(routeConfig);

            WebServer webServer = new WebServer(Port, routeConfig);

            webServer.Run();
        }
    }
}
