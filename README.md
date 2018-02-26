# SimpleClientService

This is a simple client async service that handles GET, POST, PUT, DELETE.
Main purpose for building REST service.
Data that is used for a request/response is in JSON.

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
