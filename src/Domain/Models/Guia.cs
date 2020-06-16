using System;
using System.Collections.Generic;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class Guia: Entity
    {
        public int? LoteId { get; set; }
        public Prestador Prestador { get; set; } 
        public Unidade Unidade { get; set; } 
        public string PushId { get; set; } 
        public string TokenId { get; set; } 
        public GuiaNumero GuiaNumero { get; set; } 
        public Beneficiario Beneficiario  { get; set; } 
        public decimal Valor { get; set; } 
        public DateTime Data { get; set; } 
        public string GuiaXML { get; set; } 
        public bool Deletada { get; set; }
        
        // Relacionamentos 1 - 1
        public int GuiaOrigemFK { get; set; }
        public GuiaOrigem GuiaOrigem { get; set; }
        public int GuiaStatusFK { get; set; }
        public GuiaStatus GuiaStatus { get; set; }
        public int GuiaTipoFK { get; set; }
        public GuiaTipo GuiaTipo { get; set; } 
        public int StatusCheckInFK { get; set; }
        public GuiaStatusCheckIns GuiaStatusCheckIns { get; set; } 
    }
}