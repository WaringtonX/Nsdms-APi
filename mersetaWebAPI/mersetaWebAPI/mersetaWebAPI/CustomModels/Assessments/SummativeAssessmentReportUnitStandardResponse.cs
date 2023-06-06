namespace mersetaWebAPI.CustomModels.Assessments
{
    public class SummativeAssessmentReportUnitStandardResponse
    {
        public long Id { get; set; }
        public DateTime? AssesmentDate { get; set; }
        public int? CompetenceEnum { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModerationDate { get; set; }
        public int? UnitStandardTypeEnum { get; set; }
        public long? AssessorApplicationId { get; set; }
        public long? AssessorUserId { get; set; }
        public long? CompanyLearnersId { get; set; }
        public long? ModeratorApplicationId { get; set; }
        public long? ModeratorUserId { get; set; }
        public long? SummativeAssessmentReportId { get; set; }
        public long? UnitStandardsId { get; set; }
    }
}
