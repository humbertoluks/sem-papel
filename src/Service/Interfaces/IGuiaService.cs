using Domain.Arguments;
using Domain.Helpers;
using Domain.Models;

namespace Service.Interfaces
{
    public interface IGuiaService
    {
         string GenerateXMLGuia(Guia guia, Enums.PerformerCodeType performerCodeType, string prestador,  
            BeneficiarioResponse beneficiario, MedicoResponse profissional, string procedimento);
    }
}
