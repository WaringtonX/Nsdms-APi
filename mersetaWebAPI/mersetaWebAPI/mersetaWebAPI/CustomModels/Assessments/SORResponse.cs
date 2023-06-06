namespace mersetaWebAPI.CustomModels.Assessments
{
    public class SORResponse
    {
        public long? QualificationId{ get; set; }
        public string? QualificationidString { get; set; }
        public string? QualificationTitle { get; set; }
        public string? UnitStandardTitle { get; set; }
        public long? UnitStandardId { get; set; }
        public long? UnitstdnNumberoOfCredits { get; set; }
        public int? QualificationMinimumCredits { get; set; }
        public string? UnitStandardType { get; set; }
        public string? CompetencyStatus { get; set; }
        public DateTime? AssesmentDate { get; set; }
        public string? AssessFirstname { get; set; }
        public string? AssessorLastname { get; set; }
        public string? AssessorRsaIdNumber { get; set; }
        public string? FirstName { get; set; }
        public DateTime? ModerationDate { get; set; }
        public string? ModeratorFirstName { get; set; }
        public string? ModeratorLastname { get; set; }
        public string? ModeratorRsaIdNumber { get; set; }
      
    }
}
