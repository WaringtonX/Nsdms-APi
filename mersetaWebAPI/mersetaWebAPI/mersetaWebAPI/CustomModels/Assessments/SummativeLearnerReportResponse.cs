namespace mersetaWebAPI.CustomModels.Assessments
{
    public class SummativeLearnerReportResponse
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? RsaId { get; set; }
        public string? PasswordNumber { get; set; }
        public long? CompanyLearnerId { get; set; }
        public long? EmployerId { get; set; }
        public long? CompanyId { get; set; }
        public long? Status { get; set; }
        public long? LearnerStatusId { get; set; }
        public long? QualificationId { get; set; }
        public long? InterventionTypeId { get; set; }
        public long? SummativeAssessmentId { get; set; }
        public long AssessmentProgressStatusTypeId { get; set; }
      
    }
    
}
