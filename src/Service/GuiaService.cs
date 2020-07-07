using RestSharp;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Service.Interfaces;

using Domain.Models;

namespace Service
{
  public class GuiaService : IGuiaService
  {
    private readonly IAssociadoService _AssociadoService;
    private readonly IPrestadorService _PrestadorService;
    public GuiaService(IAssociadoService AssociadoService, IPrestadorService PrestadorService)
    {
      this._AssociadoService = AssociadoService;
      this._PrestadorService = PrestadorService;
    }
    public string GenerateXMLGuia(string codigoPrestador, string cartaoBeneficiario, string numeroGuia, int unidadeId, string ufCrm, int crm, string procedimento, decimal valor)
    {
      var descriptionPrestador = _PrestadorService.PrestadorDescription(codigoPrestador);

      //var associado = new AssociadoService();

      var associadoName = _AssociadoService.SeachAssociado(cartaoBeneficiario);

      var profissional = _PrestadorService.PrestadorMedico(ufCrm, crm);

      var guiaXML = new GuiaAtendimentoXML
      {
        guideANS = "359661",
        guideTypeId = '4', // GUIA DE CONSULTA
        guideStatusId = 1,
        guideNumberHealth = "",
        providerId = codigoPrestador,
        providerUnidadeId = unidadeId.ToString(),

        guidePerformerName = descriptionPrestador,
        guidePerformerCode = codigoPrestador,

        guidePerformerCodeType = "codigoPrestadorNaOperadora",

        guidePerformerCNES = "999999",
        guideOrgimId = '3', // USAR 3 ENQUANTO A API É USADA APENAS POR TELEMEDICINA
        guideObservation = "Guia gerada no Telemedicina, com token aprovado",
        guideQueryType = "1",
        guideIndicationCrahs = "9",
        guideTableNumber = "00",

        guideProcedureNumber = procedimento,
        guideToken = "", // GERAR PUSH
        guideBeneficiaryToken = "", // GERAR TOKEN

        guideNumber = numeroGuia.ToString(),

        guideDayCare = DateTime.Now.ToString("dd/MM/yyyy"),
        guideBeneficiaryRN = "N",
        guideBeneficiaryName = associadoName,
        guideBeneficiaryNumber = cartaoBeneficiario,
        guideBeneficiaryNumberCompanion = "0",
        guideBeneficiaryCompanionRG = "",
        guideBeneficiaryCompanionName = "",
        guideBeneficiaryCompanionType = "",

        guidePerformerProfName = profissional,
        guidePerformerProfCode = "14762405825",
        guidePerformerProfAdviceUF = "35",
        guidePerformerProfAdviceCBOS = "999999",
        guidePerformerProfAdviceNumber = "87157",
        guidePerformerProfAdviceAcronym = "6",
        guideProcedures = new procedimentosXML[1]
          {
              new procedimentosXML {
                  dateExecution = DateTime.Now.ToString("dd/MM/yyyy"),
                  procedureNumber = procedimento,
                  procedureAmount = "1",
                  procedureDescription = "Descrição do Procedimento",
                  priceUnit = valor,
                  priceTotal = valor
              }
          }
      };

      XmlSerializer xsSubmit = new XmlSerializer(typeof(GuiaAtendimentoXML));
      XmlWriterSettings writeSettings = new XmlWriterSettings
      {
        Indent = true,
        OmitXmlDeclaration = false,
        Encoding = Encoding.UTF8
      };

      StringWriter sww = new StringWriter();
      XmlWriter writer = XmlWriter.Create(sww, writeSettings);
      xsSubmit.Serialize(writer, guiaXML);

      return sww.ToString();
    }
  }
}