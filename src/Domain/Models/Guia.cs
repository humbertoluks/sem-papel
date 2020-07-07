using System;
using System.Xml.Serialization;
using Domain.ValueObjects;

namespace Domain.Models
{
    [Serializable]
    public class Guia: EntityGuia
    {
        [XmlIgnore] public int? LoteId { get; set; }
        public Prestador Prestador { get; set; } 
        public Unidade Unidade { get; set; } 
        public string PushId { get; set; } 
        public string TokenId { get; set; } 
        public NumeroGuia GuiaNumero { get; set; } 
        public Beneficiario Beneficiario  { get; set; } 
        public decimal Valor { get; set; } 
        public DateTime Data { get; set; } 
        public string GuiaXML { get; set; } 
        public bool Deletada { get; set; }
        
        //Relacionamentos 1 - 1
        [XmlIgnore] public int GuiaOrigemFK { get; set; }
        [XmlIgnore] public GuiaOrigem GuiaOrigem { get; set; }
        [XmlIgnore] public int GuiaStatusFK { get; set; }
        [XmlIgnore] public GuiaStatus GuiaStatus { get; set; }
        [XmlIgnore] public int GuiaTipoFK { get; set; }
        [XmlIgnore] public GuiaTipo GuiaTipo { get; set; } 
        [XmlIgnore] public int StatusCheckInFK { get; set; }
        [XmlIgnore] public GuiaStatusCheckIns GuiaStatusCheckIns { get; set; } 
        //public int GuiaExternaId { get; set; } 
    }
}