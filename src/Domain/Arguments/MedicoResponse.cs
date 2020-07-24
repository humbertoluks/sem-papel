using System;

namespace Domain.Arguments
{
    public class MedicoResponse
    {
        public string medicoId { get; set; }
        public string medicoCRM { get; set; }
        public string medicoCRMUF { get; set; }
        public string medicoNome { get; set; }
        public Boolean medicoStaff { get; set; }
        public string medicoCpfCnpj { get; set; }
        public bool flagAtende { get; set; }
        public char flagTeleMedicina { get; set; }
    }
}
