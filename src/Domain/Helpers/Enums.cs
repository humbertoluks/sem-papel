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
            Portal,
            Telemedicina
        }
        public enum PerformerCodeType
        {
            CNPJ,
            cpf,
            codigoPrestadorNaOperadora
        }
        public enum AdviceType{
            CRAS = 1,
            COREN,
            CRF,
            CRFA,
            CREFITO,
            CRM,
            CRN,
            CRO,
            CRP,
            OUTROS
        }
        public enum DegreeKinship{
            AvoAvo,
            TioTia,
            IrmaoIrma,
            PrimoPrima,
            PadrastoMadrasta,
            Baba
        }
    }
}
