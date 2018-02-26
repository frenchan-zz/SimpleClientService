using System.Net;

namespace SimpleClientService.Models
{
    public class ServiceCredential : NetworkCredential
    {
        public ServiceCredential(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
