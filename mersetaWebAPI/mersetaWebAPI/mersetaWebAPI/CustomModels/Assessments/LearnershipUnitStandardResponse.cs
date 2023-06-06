namespace mersetaWebAPI.CustomModels.Assessments
{
    public class LearnershipUnitStandardResponse
    {
        public long Id { get; set; }
        public long UnitStandardTypeId { get; set; }
        public long LearnershipId { get; set; }
        public long UnitStandardsId { get; set; }
        public long SaqaUnitStandardId { get; set; }
        public string? AbetBandDescription { get; set; }
        public string? EtqaName { get; set; }
        public string? EtqaAcronym { get; set; }
        public string? Field { get; set; }
        public string? FieldDescription { get; set; }
        public string? NqfLevelDescription { get; set; }
        public string? NqfLevelg2DESCRIPTION { get; set; }
        public string? RegistrationStatusDesc { get; set; }
        public string? SaqaDecisionNumber { get; set; }
        public string? SgbName { get; set; }
        public string? SubFieldDescription { get; set; }
        public int TrainOutPeriod { get; set; }
        public int TransitionPeriod { get; set; }
        public long UnitstdNumberOfCredits { get; set; }    
    }
}
