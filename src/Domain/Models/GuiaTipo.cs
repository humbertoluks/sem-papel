using System.Collections.Generic;

namespace Domain.Models
{
    public class GuiaTipo: Entity
    {
        public string Descricao { get; set; }
        public int? Local { get; set; }
        public IEnumerable<Guia> Guias { get; set; }
    }
}