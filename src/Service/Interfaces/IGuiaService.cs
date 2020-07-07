namespace Service.Interfaces
{
    public interface IGuiaService
    {
         string GenerateXMLGuia(string codigoPrestador, string cartaoBeneficiario, 
            string numeroGuia, int unidadeId, string ufCrm, int crm, 
            string procedimento, decimal valor);
    }
}