using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Config;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Utils.SmsC
{
    public class SmscHelper
    {
        public SmsSettings _smsSettings;

        public SmscHelper(IConfiguration configuration)
        {
            this._smsSettings = configuration.GetSection("SmsSettings").Get<SmsSettings>();
        }

        public async Task<string> SendSms(string phone, string message)
        {
            var qb = new QueryBuilder();
            qb.Add("login", _smsSettings.Login);
            qb.Add("psw", _smsSettings.Psw);
            qb.Add("phones", phone);
            qb.Add("mes", message);

            var baseUri = new Uri(_smsSettings.Url).GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
            var fullUri = baseUri + qb.ToQueryString();

            var client = new HttpClient();
            var response = await client.GetAsync(fullUri);


            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetNumberInfo(string phone)
        {
            var qb = new QueryBuilder();
            qb.Add("get_operator", "1");
            qb.Add("login", _smsSettings.Login);
            qb.Add("psw", _smsSettings.Psw);
            qb.Add("phone", phone);

            var baseUri = new Uri(_smsSettings.Url).GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
            var fullUri = baseUri + qb.ToQueryString();

            var client = new HttpClient();
            var response = await client.GetAsync(fullUri);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
