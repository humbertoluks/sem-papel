namespace Domain.ValueObjects
{
    public class Medico: ValueObject
    {
        public string Id { get; set; }
        public string CRM { get; set; }
        public string Nome { get; set; }
        public string Staff { get; set; }
        public string CpfCnpj { get; set; }
        public bool Atende { get; set; }
    }
}