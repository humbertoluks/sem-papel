using System;

namespace Domain.Dtos
{
    public class GuiaDto
    {
        public string CodigoPrestador { get; set; } 
        public string IdUnidade { get; set; } 
        public string BeneficiarioCartao { get; set; } 
        public string ProfissionalCRM { get; set; } 
        public string ProfissionalUFCRM { get; set; } 
        public decimal Valor { get; set; } 
        public string Procedimento { get; set; }
    }
}