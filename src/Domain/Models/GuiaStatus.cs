using System.Collections.Generic;

namespace Domain.Models
{
    public class GuiaStatus: Entity
    {
        public string Descricao { get; set; }
        public ICollection<Guia> Guias { get; set; }
    }
}