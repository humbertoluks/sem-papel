using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Linq;

using Domain.Arguments;
using Domain.Models;
using Service.Interfaces;

namespace Service
{
    public class PrestadorService: IPrestadorService
    {
        public readonly WebApiConfiguration _config;
        private static RestClient client;
        public PrestadorService(IOptions<WebApiConfiguration> options)
        {
            _config = options.Value;
            client = new RestClient(_config.MWWebApi);
        }
        private string login()
        {
            RestRequest login = new RestRequest("/oauth/token", Method.POST);
            var apiBody = "grant_type=password&password=SuperPowerUser&username=MySuperP@ss!";
            login.AddParameter("text/xml", apiBody, ParameterType.RequestBody);
            
            var response_login = client.Execute(login);
            if(response_login.StatusCode != System.Net.HttpStatusCode.OK) return "";
            JObject tokenObj = JObject.Parse(response_login.Content);

            return (string)tokenObj["access_token"];
        }
        
        public string PrestadorDescription(string codigoPrestador)
        {
            var token = login();
            RestRequest request = new RestRequest($"/ContratoPrestador/GetPrestador/{codigoPrestador}", Method.GET);
            request.Parameters.Clear();
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            
            IRestResponse<string> response = client.Execute<string>(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) return "";
            
            var prestadorObj = JObject.Parse(response.Content);

            return (string)prestadorObj["prestadorNome"];
        }

        public MedicoResponse PrestadorMedico(string codigoPrestador, string ufCrm, int nrCrm, string nomeMedico)
        {
            var token = login();
            RestRequest request = new RestRequest($"/ContratoPrestador/GetPrestadorMedicos", Method.GET);
            request.Parameters.Clear();
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));

            request.AddParameter("cdPrestador", codigoPrestador, ParameterType.QueryString);
            request.AddParameter("ufPrestador", ufCrm, ParameterType.QueryString);
            request.AddParameter("crmMedico", nrCrm, ParameterType.QueryString);
            request.AddParameter("nomeMedico", nomeMedico, ParameterType.QueryString);

            IRestResponse<string> response = client.Execute<string>(request);
            
            if(response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == "null") return null;

            var medicos = JArray.Parse(response.Content);
            
            return medicos.FirstOrDefault()?.ToObject<MedicoResponse>();
        }
    }
}
