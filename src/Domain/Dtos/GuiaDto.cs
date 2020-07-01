using System;

namespace Domain.Dtos
{
    public class GuiaDto
    {
        public string CodigoPrestador { get; set; } 
        public string IdUnidade { get; set; } 
        public string PushId { get; set; } 
        public string TokenId { get; set; } 
        public string CarteirinhaBeneficiario { get; set; } 
        public decimal Valor { get; set; } 
        public DateTime Data { get; set; } 
        public string GuiaXML { get; set; } 
        
        public int GuiaOrigemId { get; set; }
        public int GuiaStatusId { get; set; }
        public int GuiaTipoId { get; set; }
        public int StatusCheckInId { get; set; }
    }
}