using Domain.ValueObjects;

namespace Domain.Dtos.Push
{
    public class PushRequest
    {
        public string CodAtendimento { get; set; }
        public IntPrestador Prestador { get; set; }
        public IntAssociado Associado { get; set; }
    }

    public class IntPrestador
    {
        public string Endereco { get; set; }
        public string CodPrestador { get; set; }
        public string NomePrestador { get; set; }
        public string NomeProfissional { get; set; }

        public Localizacao Localizacao { get; set; }
    }

    public class IntAssociado
    {
        public string CodAssociado { get; set; }
        public string CodAcompanhante { get; set; }

        public Localizacao Localizacao { get; set; }
    }
}