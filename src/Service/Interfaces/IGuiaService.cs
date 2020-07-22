using Domain.Models;
using Domain.Helpers;

namespace Service.Interfaces
{
    public interface IGuiaService
    {
         string GenerateXMLGuia(Guia guia, string prestador, Enums.PerformerCodeType performerCodeType, 
        string proficionalUFCRM, int profissionalCRM, string profissional, string procedimento);
    }
}