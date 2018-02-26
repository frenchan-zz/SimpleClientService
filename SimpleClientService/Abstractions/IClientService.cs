using SimpleClientService.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleClientService.Abstractions
{
    public interface IClientService
    {
        Task<ApiResult> SimpleExecute(Uri uri, HttpMethod method, string language = null, object payload = null);
        Task<ApiResult> Execute(Uri uri, HttpMethod method, string language, ServiceCredential credential = null, string apiKey = null, object payload = null);
        Task<ApiResult> Execute(HttpClientHandler httpClientHandler, HttpRequestMessage httpRequestMessage, object payload = null);
    }
}
