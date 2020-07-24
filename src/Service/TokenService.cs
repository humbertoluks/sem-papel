using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using Domain.Models;
using Service.Interfaces;

namespace Service
{
    public class TokenService: ITokenService
    {
        public readonly WebApiConfiguration _config;
        private static RestClient client;
        public TokenService(IOptions<WebApiConfiguration> options)
        {
            _config = options.Value;
            client = new RestClient(_config.HostToken);
        }
        public string GetTokenCode(string codBeneficiario)
        {
            RestRequest request = new RestRequest("/TokenSemPapel/generate-otp", Method.POST);
            request.RequestFormat = DataFormat.Json;
            
            JObject requestToken = new JObject{
                {"SecretKey", codBeneficiario}
            };

            request.AddJsonBody(requestToken.ToString());
            
            IRestResponse<string> response = client.Execute<string>(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) return "";

            var result = JObject.Parse(response.Content);

            return (string)result["tokenGenerate"];
        }
    }
}
