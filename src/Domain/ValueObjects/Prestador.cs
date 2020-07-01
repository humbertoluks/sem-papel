using Domain.Models;

namespace Domain.ValueObjects
{
    public class Prestador
    {
        public string Codigo { get; set; }
        public int? LoginId { get; set; }

        // public Prestador(string codigo)
        // {
        //     Codigo = codigo;
        //     // LoginId = loginId;
        // }
    }
}