using System.Text.RegularExpressions;
using Domain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using Service.Interfaces;

using Domain.Arguments;

namespace Service
{
    public class AssociadoService: IBeneficiarioService
    {
        public readonly WebApiConfiguration _config;
        private static RestClient client;
        public AssociadoService(IOptions<WebApiConfiguration> options)
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
        public BeneficiarioResponse SeachBeneficiario(string cartaoBeneficiario)
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

            IRestResponse<string> response = client.Execute<string>(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) return null;
            var beneficiario = response.Content.Split('-');

            var guideBeneficiaryNumber = beneficiario[0].Remove(0,2);
            var guideBeneficiarySequency = Regex.Match(beneficiario[0],@"(.{2})\s*$").ToString();
            var guideBeneficiaryName = beneficiario[1];
            var guideBeneficiaryCompanionRG = beneficiario[4];

            var result = new BeneficiarioResponse{
                guideBeneficiaryName = guideBeneficiaryName,
                guideBeneficiaryNumber = guideBeneficiaryNumber,
                guideBeneficiarySequency = guideBeneficiarySequency,
                guideBeneficiaryCompanionRG = guideBeneficiaryCompanionRG
            };
            return result;
        }
    }
}
