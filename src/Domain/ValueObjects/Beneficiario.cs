using Domain.Models;

namespace Domain.ValueObjects
{
    public class Beneficiario
    {
        public string Cartao { get; set; }
        public string Nome { get; set; } 

        public Beneficiario(string cartao, string nome)
        {
            // if (cartao.Length < 8)
            //     throw new DomainException("Cartao inválido");

            // if (nome.Length < 3)
            //     throw new DomainException("Nome inválido");

            Cartao = cartao;
            Nome = nome;
        }

        public override string ToString()
        {
            return $"{Nome} - {Cartao}";
        }
    }
}