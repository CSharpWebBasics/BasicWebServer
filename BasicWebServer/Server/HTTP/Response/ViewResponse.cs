﻿using System;
using BasicWebServer.Server.Contracts;
using BasicWebServer.Server.Enums;


namespace BasicWebServer.Server.HTTP.Response
{
    public class ViewResponse : HttpResponse
    {
        private readonly IView view;

        public ViewResponse(HttpStatusCode httpStatusCode, IView view)
                    : base()
        {
            this.StatusCode = httpStatusCode;
            this.view = view;
        }

        public override string ToString()
        {
            int codeOfStatus = (int)this.StatusCode;

            string result = base.ToString();

            if (codeOfStatus < 300 || codeOfStatus >= 400)
            {
                result += Environment.NewLine + this.view.View();
            }

            return result;
        }
    }
}
