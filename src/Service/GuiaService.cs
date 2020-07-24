using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Domain.Arguments;
using Domain.Models;
using Domain.Helpers;
using Service.Interfaces;

namespace Service
{
  public class GuiaService : IGuiaService
  {
    private readonly IBeneficiarioService _AssociadoService;
    private readonly IPrestadorService _PrestadorService;
    public GuiaService(IBeneficiarioService AssociadoService, IPrestadorService PrestadorService)
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
    public string GenerateXMLGuia(Guia guia, Enums.PerformerCodeType performerCodeType, string prestador,  
         BeneficiarioResponse beneficiario, MedicoResponse profissional, string procedimento)
    {
      var guiaXML = new guiaAtendimentoObj
      {
        guideANS = Consts.CodeANSOmint,
        guideTypeId = guia.GuiaTipoFK,
        guideStatusId = guia.GuiaStatusFK,
        guideOrgimId = guia.GuiaOrigemFK,
        
        guideId = 0, 
        guideDayCare = guia.Data.ToString("dd/MM/yyyy"),
        
        providerId = guia.Prestador.Codigo,
        providerUnidadeId = guia.Unidade.Id.ToString(),

        guidePerformerName = prestador,
        guidePerformerCode = guia.Prestador.Codigo,
        guidePerformerCodeType = performerCodeType.ToString(),
        guidePerformerCNES = Consts.CodeCNESConsulta,
        
        guideQueryType = "1",
        guideIndicationCrahs = "9",
        guideTableNumber = "00",
        guideObservation = string.Format(Consts.GuideObservation,guia.PushId, guia.TokenId),

        guideToken = guia.PushId,
        guideBeneficiaryToken = guia.TokenId,
        guideCheckingStatus = guia.StatusCheckInFK,

        guideNumber = guia.GuiaNumero.Numero,
        guideNumberHealth = guia.GuiaNumero.NumeroOperadora,

        guideBeneficiaryRN     = "N",
        guideBeneficiaryName   = guia.Beneficiario.Nome,
        guideBeneficiaryNumber = guia.Beneficiario.Cartao,
        guideBeneficiaryNumberCompanion = "0",
        guideBeneficiaryCompanionRG     = beneficiario?.guideBeneficiaryCompanionRG,
        guideBeneficiaryCompanionName   = "",
        guideBeneficiaryCompanionType   = "",
        
        guidePerformerProfRede = true,
        
        guidePerformerProfName     = profissional?.medicoNome,
        guidePerformerProfCode     = profissional?.medicoCpfCnpj.Trim(),
        guidePerformerProfAdviceUF = profissional?.medicoCRMUF,
        guidePerformerProfAdviceCBOS    = profissional != null ? Consts.CodeCBOSConsulta: null,
        guidePerformerProfAdviceNumber  = profissional?.medicoCRM.Trim(),
        guidePerformerProfAdviceAcronym = profissional != null ? ((int)(Enums.AdviceType)System.Enum.Parse(typeof(Enums.AdviceType), "CRM")).ToString(): null,

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
        
        guideBlocked = false
      };

      return ToString(guiaXML, typeof(guiaAtendimentoObj), Encoding.UTF8);
    }
  }
}
