namespace Domain.Helpers
{
    public static class Enums
    {
        public enum StatusGuia
        {
            Aberta = 1,
            Fechada,
            NaoValidada
        }
        public enum TypeGuia
        {
            SPSADT = 1,
            ResumoInternacao,
            Honorarios, 
            Consulta, 
            Odontologia
        }
        public enum StatusCheckIns
        {
            Valido = 1,
            NaoValido,
            SemResposta,
            AtendimentoMenorIdade
        }
        public enum SourceInterface
        {
            URL,
            PORTAL,
            TELEMEDICINA
        }
        public enum PerformerCodeType
        {
            CNPJ,
            cpf,
            codigoPrestadorNaOperadora
        }
    }
}