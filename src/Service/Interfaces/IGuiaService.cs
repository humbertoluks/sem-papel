using Domain.Enumerations;
using Domain.Models;

namespace Service.Interfaces
{
    public interface IGuiaService
    {
         string GenerateXMLGuia(Guia guia, string prestador, PerformerCodeType performerCodeType, 
        string proficionalUFCRM, int profissionalCRM, string profissional, string procedimento);
    }
}