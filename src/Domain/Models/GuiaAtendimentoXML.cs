using System;

namespace Domain.Models
{
    public class guiaAtendimentoObj
    {
        public int lotId { get; set; }
        public string providerId { get; set; }
        public string providerUnidadeId { get; set; }
        public int guideOrgimId { get; set; }
        public int providerLoginId { get; set; }
        public int guideId { get; set; }
        public int guideTypeId { get; set; }
        public int guideStatusId { get; set; }
        public int guideProcedureOk { get; set; }
        public int guideBatchClosure { get; set; }
        public int guideCheckingStatus { get; set; }
        public string lotNumber { get; set; }
        public string guideXML { get; set; }
        public string guideANS { get; set; }
        public string guideToken { get; set; }
        public string guideNumber { get; set; }
        public string guideNumberHealth { get; set; }
        public string guideNumberPrincipal { get; set; }
        public string guideInvoice { get; set; }
        public string guideDayCare { get; set; }
        public string guidePassword { get; set; }
        public string guideDateIssuance { get; set; }
        public string guideBeneficiaryRN { get; set; }
        public string guideBeneficiaryName { get; set; }
        public string guideBeneficiaryNumber { get; set; }
        public string guideBeneficiaryNumberCNS { get; set; }
        public string guideBeneficiaryNumberCompanion { get; set; }
        public string guideBeneficiaryCompanionRG { get; set; }
        public string guideBeneficiaryCompanionName { get; set; }
        public string guideBeneficiaryCompanionType { get; set; }
        public string guideRequestorName { get; set; }
        public string guideRequestorCode { get; set; }
        public string guideRequestorCodeType { get; set; }
        public string guideRequestorCharaterService { get; set; }
        public string guideRequestorClinicalIndication { get; set; }
        public string guideRequestorProfName { get; set; }
        public string guideRequestorProfAdviceUF { get; set; }
        public string guideRequestorProfAdviceCBOS { get; set; }
        public string guideRequestorProfAdviceNumber { get; set; }
        public string guideRequestorProfAdviceAcronym { get; set; }
        public string guidePerformerName { get; set; }
        public string guidePerformerCode { get; set; }
        public string guidePerformerCNES { get; set; }
        public string guidePerformerCodeType { get; set; }
        public string guidePerformerProfName { get; set; }
        public string guidePerformerProfCode { get; set; }
        public string guidePerformerProfAdviceUF { get; set; }
        public string guidePerformerProfAdviceCBOS { get; set; }
        public string guidePerformerProfAdviceNumber { get; set; }
        public string guidePerformerProfAdviceAcronym { get; set; }
        public string guideLocalContractorCode { get; set; }
        public string guideLocalContractorCNES { get; set; }
        public string guideLocalContractorName { get; set; }
        public string guideLocalContractorCodeType { get; set; }
        public string guideDateStopBilling { get; set; }
        public string guideDateStartBilling { get; set; }
        public string guideQueryType { get; set; }
        public string guideServiceType { get; set; }
        public string guideClosingReason { get; set; }
        public string guideIndicationCrahs { get; set; }
        public string guideObservation { get; set; }
        public string guideDateAuthorization { get; set; }
        public string guideRequestorDate { get; set; }
        public string guidePasswordExpiration { get; set; }
        public string guideTableNumber { get; set; }
        public string guideProcedureNumber { get; set; }
        public string guidePriceGrandTotalFormat { get; set; }
        public string guideDocument { get; set; }
        public string guideDocumentGed { get; set; }
        public string guideDocumentName { get; set; }
        public procedimentosXML[] guideProcedures { get; set; }
        public outrosProcedimentosXML[] guideOtherExpenses { get; set; }
        public decimal guidePriceProcedure { get; set; }
        public decimal guidePriceDaily { get; set; }
        public decimal guidePriceRatesRents { get; set; }
        public decimal guidePriceMaterials { get; set; }
        public decimal guidePriceMedicines { get; set; }
        public decimal guidePriceOPME { get; set; }
        public decimal guidePriceMedicinalGases { get; set; }
        public decimal guidePriceGrandTotal { get; set; }
        public Boolean guideBlocked { get; set; }
        public Boolean guidePerformerProfRede { get; set; }
        public string guideBeneficiaryToken { get; set; }
    }

    public class procedimentosXML
    {
        public string hourStop { get; set; }
        public string hourStart { get; set; }
        public string technique { get; set; }
        public string accessRoad { get; set; }
        public string tableNumber { get; set; }
        public string dateExecution { get; set; }
        public string procedureNumber { get; set; }
        public string procedureAmount { get; set; }
        public string procedureDescription { get; set; }
        public string reductionAccretion { get; set; }
        public decimal priceUnit { get; set; }
        public decimal priceTotal { get; set; }
        public membroXML[] procedureMembers { get; set; }
    }
    public class outrosProcedimentosXML
    {
        public string expensesType { get; set; }
        public string expensesDate { get; set; }
        public string expensesHourStop { get; set; }
        public string expensesHourStart { get; set; }
        public string expensesUnit { get; set; }
        public string expensesTableNumber { get; set; }
        public string expensesProcedureNumber { get; set; }
        public string expensesProcedureAmount { get; set; }
        public string expensesReductionAccretion { get; set; }
        public string expensesProcedureDescription { get; set; }
        public string expensesMaker { get; set; }
        public string expensesANVISA { get; set; }
        public string expensesAuthorization { get; set; }

        public decimal expensesPriceUnit { get; set; }
        public decimal expensesPriceTotal { get; set; }
    }
    public class membroXML
    {
        public string memberName { get; set; }
        public string memberCode { get; set; }
        public string memberDegree { get; set; }
        public string memberCodeType { get; set; }
        public string memberCBOS { get; set; }
        public string memberAdviceUF { get; set; }
        public string memberAdviceCode { get; set; }
        public string memberAdviceAcronym { get; set; }
    }
}