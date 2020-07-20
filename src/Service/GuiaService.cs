using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Domain.Enumerations;

//using Domain.Enumerations;
using Domain.Models;
using Service.Interfaces;

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

    public static string ToString(object source, Type type, Encoding encoding)
    {
        string content;
        using (var sww = new StringWriter()){
            XmlSerializer xsSubmit = new XmlSerializer(type);
            XmlWriterSettings writeSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                Encoding = encoding
            };
            using (var writer = XmlWriter.Create(sww, writeSettings))
            {
                xsSubmit.Serialize(writer, source);
                writer.Flush();
                content = sww.ToString();
            }
        }

        return content;
    }
    public string GenerateXMLGuia(Guia guia, string prestador, PerformerCodeType performerCodeType, 
        string proficionalUFCRM, int profissionalCRM, string profissional, string procedimento)
    {
      var guiaXML = new guiaAtendimentoObj
      {
        guideANS = "359661",

        guideTypeId = guia.GuiaTipoFK,
        guideStatusId = guia.GuiaStatusFK,
        guideOrgimId = guia.GuiaOrigemFK,
        guideCheckingStatus = guia.StatusCheckInFK,

        guideId = 0,
        guideDayCare = guia.Data.ToString("dd/MM/yyyy"),
        
        providerId = guia.Prestador.Codigo,
        providerUnidadeId = guia.Unidade.Id.ToString(),

        guidePerformerName = prestador,
        guidePerformerCode = guia.Prestador.Codigo,
        guidePerformerCodeType = performerCodeType.ToString(),
        guidePerformerCNES = "999999",
        
        guideObservation = "Guia gerada no Telemedicina, com token aprovado",
        guideQueryType = "1",
        guideIndicationCrahs = "9",
        guideTableNumber = "00",

        guideToken = "", // GERAR PUSH
        guideBeneficiaryToken = "", // GERAR TOKEN
        
        guideNumber = guia.GuiaNumero.Numero,
        guideNumberHealth = guia.GuiaNumero.NumeroOperadora,

        guideBeneficiaryRN = "N",
        guideBeneficiaryName = guia.Beneficiario.Nome,
        guideBeneficiaryNumber = guia.Beneficiario.Cartao,
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

        guideProcedureOk = 1,
        guideProcedureNumber = procedimento,
        guideProcedures = new procedimentosXML[1]
        {
            new procedimentosXML {
                dateExecution = guia.Data.ToString("dd/MM/yyyy"),
                procedureNumber = procedimento,
                procedureAmount = "1",
                procedureDescription = "Descrição do Procedimento",
                priceUnit = guia.Valor,
                priceTotal = guia.Valor
            }
        },
        guidePriceProcedure = guia.Valor,
        guidePriceGrandTotal = guia.Valor,
        guideBlocked = false,
        guidePerformerProfRede = true
      };

      return ToString(guiaXML, typeof(guiaAtendimentoObj), Encoding.UTF8);
    }
  }
}