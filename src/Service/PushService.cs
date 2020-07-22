using Service.Interfaces;

using Domain.Models;
using Domain.Dtos.Push;
using RestSharp;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
namespace Service
{
    public class PushService: IPushService
    {
        public readonly WebApiConfiguration _config;
        private static RestClient client;
        public PushService(IOptions<WebApiConfiguration> options)
        {
            _config = options.Value;
            client = new RestClient(_config.Host);
        }

        public string Post(PushRequest pushRequest)
        {

            RestRequest request = new RestRequest("/api/atendimento-sem-papel/gerar-chave-atendimento", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(pushRequest);
            
            IRestResponse<string> response = client.Execute<string>(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) return "";

            var result = JObject.Parse(response.Content);

            return (string)result["ChaveAtendimento"];
        }
    }
}