# SimpleClientService

This is a simple client async service that handles GET, POST, PUT, DELETE.
Main purpose for building REST service.
~~Data that is used for a request/response is in JSON.~~

# Configure
Register the IClientService on the Startup.cs
```sh
services.AddSingleton<IClientService, ClientApiService>();
```

Appsettings.json requires following properties:
```sh
"ClientService": {
    "UserName": "",
    "Password": "",
    "BaseApiUrl": "",
    "Language": "",
    "ApiKey": "",
    "CustomHeaderType": "",
    "CustomHeaderValue": "",
  }
```

Inject the service in the constructor:
```sh
public WeatherService(IClientService clientService, IConfiguration configuration)
{
    _clientService = clientService;
    _configuration = configuration;
}
```

Sample use of the service:
```sh
public async Task<string> GetWeatherForCity(string city)
{
    var uri = new Uri($"{ _configuration["ClientService:BaseApiUrl"] }weather")
         .AddParameter("q", city);

    var result = await _clientService.SimpleExecute(uri, HttpMethod.Get);

    return result.ResponseBody;
}
```
