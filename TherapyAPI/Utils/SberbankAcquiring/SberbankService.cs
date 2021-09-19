using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Config;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Utils.SberbankAcquiring.Models.Request;
using Utils.SberbankAcquiring.Models.Response;

namespace Utils.SberbankAcquiring
{
    public class SberbankService
    {
        private readonly Uri Host;

        public SberbankService(IConfiguration configuration)
        {
            var sberbankSettings = configuration.GetSection("SberbankApiSettings").Get<SberbankApiSettings>();
            this.Host =  new Uri(sberbankSettings.Host);
        }

        public async Task<RegisterDOResponse> RegisterDO(RegisterDORequest request)
        {
            using (var client = new HttpClient())
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var content = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(request, serializerSettings));
                var uri = new Uri(Host, "payment/rest/register.do");
                var response = await client.PostAsync(uri, new FormUrlEncodedContent(content));
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<RegisterDOResponse>(responseContent);
            }
        }
    }
}
