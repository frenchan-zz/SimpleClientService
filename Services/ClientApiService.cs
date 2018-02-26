using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleClientService.Abstractions;
using SimpleClientService.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClientService.Services
{
    public class ClientApiService : IClientService
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public ClientApiService(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public async Task<ApiResult> SimpleExecute(Uri uri, HttpMethod method, string language = null, object payload = null)
        {
            var credentials = new ServiceCredential(
                _configuration["ClientService:UserName"],
                _configuration["ClientService:Password"]);

            language = !string.IsNullOrEmpty(language)
                ? language
                : _configuration["ClientService:Language"];

            return await Execute(uri, method, language, credentials, _configuration["ClientService:ApiKey"], payload);
        }

        public async Task<ApiResult> Execute(Uri uri, HttpMethod method, string language, ServiceCredential credential = null, string apiKey = null, object payload = null)
        {
            var httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true
            };

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri
            };

            httpRequestMessage.Headers.Add("Cache-Control", "no-cache");

            if (credential != null)
            {

                var credentials = string.Format("{0}:{1}", credential.UserName, credential.Password);
                var credentialsBytes = Encoding.ASCII.GetBytes(credentials);
                var auth = Convert.ToBase64String(credentialsBytes);

                httpRequestMessage.Headers.Add("Authorization", "Basic " + auth);
            }

            if (!string.IsNullOrEmpty(apiKey))
            {
                httpRequestMessage.Headers.Add("x-api-key", apiKey);
            }

            if (!string.IsNullOrEmpty(language))
            {
                httpRequestMessage.Headers.Add("Accept-Language", language);
            }

            if(!string.IsNullOrEmpty(_configuration["ClientService:CustomHeaderType"]))
            {
                httpRequestMessage.Headers.Add(_configuration["ClientService:CustomHeaderType"], _configuration["ClientService:CustomHeaderValue"]);
            }

            return await Execute(httpClientHandler, httpRequestMessage, payload);
        }

        public async Task<ApiResult> Execute(HttpClientHandler httpClientHandler, HttpRequestMessage httpRequestMessage, object payload = null)
        {
            try
            {
                using (var client = new HttpClient(httpClientHandler))
                {
                    if (payload != null)
                    {
                        var requestJson = JsonConvert.SerializeObject(payload);
                        httpRequestMessage.Content = new StringContent(requestJson, Encoding.Default, "application/json");
                    }

                    var response = await client.SendAsync(httpRequestMessage).ConfigureAwait(false);

                    return response.IsSuccessStatusCode
                        ? new ApiResult(response.IsSuccessStatusCode, response.StatusCode, await response.Content.ReadAsStringAsync().ConfigureAwait(false))
                        : new ApiResult(response.IsSuccessStatusCode, response.StatusCode);
                }
            }
            catch (Exception exception)
            {
                var log = _loggerFactory.CreateLogger("Client API Service");
                log.LogError($"There has been a problem making the request to the API.{exception.Message}");

                return new ApiResult(exception);
            }
        }
    }
}
