﻿
namespace BasicWebServer.Server.HTTP.Contracts
{
    public interface IHttpResponse
    {
        void AddHeader(string key, string value);
    }
}
