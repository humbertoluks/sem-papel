using Newtonsoft.Json.Linq;
using RestSharp;

using Service.Interfaces;

namespace Service
{
    public class AssociadoService: IAssociadoService
    {
        private static RestClient _Client = new RestClient("http://omtseg-prod.omintseguros.com.br/MW.WebApi.SemPapel");
        public AssociadoService(){ }
        private string login()
        {
            RestRequest login = new RestRequest("/oauth/token", Method.POST);
            var apiBody = "grant_type=password&password=SuperPowerUser&username=MySuperP@ss!";
            login.AddParameter("text/xml", apiBody, ParameterType.RequestBody);

            var response_login = _Client.Execute(login);
            if(response_login.StatusCode != System.Net.HttpStatusCode.OK) return "";
            JObject tokenObj = JObject.Parse(response_login.Content);

            return (string)tokenObj["access_token"];
        }
        public string SeachAssociado(string cartaoBeneficiario)
        {
            var token = login();
            var cartao = cartaoBeneficiario.Substring(0, 8);
            var sequencial = cartaoBeneficiario.Substring(8,2);

            RestRequest request = new RestRequest($"Associado/GetBeneficiariosPrestador", Method.GET);

            request.Parameters.Clear();
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));

            request.AddParameter("pCdPrestador", ParameterType.QueryString);
            request.AddParameter("pNrTitulo", cartao, ParameterType.QueryString);
            request.AddParameter("pNrSqTitulo", sequencial, ParameterType.QueryString);
            request.AddParameter("pNome", ParameterType.QueryString);
            request.AddParameter("pNrCPF", ParameterType.QueryString);

            IRestResponse<string> response = _Client.Execute<string>(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) return "";
            
            var guideBeneficiaryName = response.Content.Split('-')[1];
            var guideBeneficiaryNumber = response.Content.Split('-')[0].Remove(0,2);

            return guideBeneficiaryName;
        }
    }
}