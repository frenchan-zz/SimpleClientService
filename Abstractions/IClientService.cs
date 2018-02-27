using SimpleClientService.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleClientService.Abstractions
{
    public interface IClientService
    {
        Task<ApiResult> SimpleExecute(Uri uri, HttpMethod method, HttpContent payload = null, string language = null);
        Task<ApiResult> Execute(Uri uri, HttpMethod method, HttpContent payload = null, ServiceCredential credential = null, string language = null,  string apiKey = null);
        Task<ApiResult> Execute(HttpClientHandler httpClientHandler, HttpRequestMessage httpRequestMessage, HttpContent payload = null);
    }
}
