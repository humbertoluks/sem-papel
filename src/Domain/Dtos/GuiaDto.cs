using System;

namespace Domain.Dtos
{
    public class GuiaDto
    {
        public string CodigoPrestador { get; set; } 
        public string IdUnidade { get; set; } 
        public string PushId { get; set; } 
        public string TokenId { get; set; } 
        public string BeneficiarioCartao { get; set; } 
        public string Beneficiario { get; set; } 
        public string ProfissionalUFCRM { get; set; } 
        public string ProfissionalCRM { get; set; } 
        public decimal Valor { get; set; } 
        public DateTime Data { get; set; } 
        public string GuiaXML { get; set; } 
        
        public int GuiaOrigemId { get; set; }
        public int GuiaStatusId { get; set; }
        public int GuiaTipoId { get; set; }
        public int StatusCheckInId { get; set; }
        public string Procedimento { get; set; }
        public string DescricaoProcedimento { get; set; }
    }
}