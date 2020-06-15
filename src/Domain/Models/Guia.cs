using System;
using System.Collections.Generic;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class Guia: Entity
    {
        public Prestador Prestador { get; set; } 
        public Unidade Unidade { get; set; } 
        public string PushId { get; set; } 
        public string TokenId { get; set; } 
        public string Numero { get; set; } 
        public Beneficiario Beneficiario  { get; set; } 
        public decimal Valor { get; set; } 
        public DateTime Data { get; set; } 
        public string GuiaXML { get; set; } 
        public int IdGuiaExterno { get; set; } 

        // Relacionamentos 1 - 1
        public int GuiaStatusId { get; set; }
        public GuiaStatus GuiaStatus { get; set; }
        public int GuiaTipoId { get; set; }
        public GuiaTipo GuiaTipo { get; set; } 
        public int StatusCheckInId { get; set; }
        public GuiaStatusCheckIns GuiaStatusCheckIns { get; set; } 
    }
}