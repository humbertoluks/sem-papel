using System.Collections.Generic;

namespace Domain.Models
{
  public class GuiaOrigem : Entity
  {
    public string Descricao { get; set; }
    //public ICollection<Guia> Guias { get; set; }
    public IEnumerable<Guia> Guias { get; set; }
  }
}