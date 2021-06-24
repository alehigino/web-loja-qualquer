using System;
using System.Net;

namespace LojaQualquer.Web.Extensions
{
    public class HttpRequestException : Exception
    {
        public HttpStatusCode StatusCode;

        public HttpRequestException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}