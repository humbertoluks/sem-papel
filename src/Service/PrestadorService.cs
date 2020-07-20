using Newtonsoft.Json.Linq;
using RestSharp;

using Service.Interfaces;

namespace Service
{
    public class PrestadorService: IPrestadorService
    {
        private static RestClient client = new RestClient("http://omtseg-homolog.omintseguros.com.br/MW.WebApi.Ext");
        public PrestadorService(){ }
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

        public string PrestadorMedico(string codigoPrestador, string ufCrm, int nrCrm, string nomeMedico)
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
            if(response.StatusCode != System.Net.HttpStatusCode.OK) return "";
            
            var medicos = JArray.Parse(response.Content);
            var medico = medicos.First;
            //var medico = JsonConvert.DeserializeObject<Medico>(response.Content);

            return (string)medicos.First["medicoNome"];
        }
    }
}