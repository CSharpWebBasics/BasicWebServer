﻿
using BasicWebServer.Server.Contracts;

namespace BasicWebServer.Application.Views
{
    public class HomeIndexView : IView
    {
        public string View()
        {
            return "<body><h1>Welcome!</h1></body>";
        }
    }
}
