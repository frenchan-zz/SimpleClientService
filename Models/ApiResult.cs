using SimpleClientService.Abstractions;
using System;
using System.Net;

namespace SimpleClientService.Models
{
    public class ApiResult : IClientResult
    {
        public bool IsSuccessStatusCode { get; set; }

        public HttpStatusCode ResponseStatusCode { get; set; }

        public string ResponseBody { get; set; }

        internal ApiResult()
        {
        }

        internal ApiResult(bool isSuccessStatusCode, HttpStatusCode responseStatusCode, string responseBody = null)
        {
            IsSuccessStatusCode = isSuccessStatusCode;
            ResponseStatusCode = responseStatusCode;
            ResponseBody = responseBody;
        }

        internal ApiResult(Exception exception)
        {
            IsSuccessStatusCode = false;
            ResponseStatusCode = HttpStatusCode.InternalServerError;
            ResponseBody = exception.Message;
        }
    }
}
