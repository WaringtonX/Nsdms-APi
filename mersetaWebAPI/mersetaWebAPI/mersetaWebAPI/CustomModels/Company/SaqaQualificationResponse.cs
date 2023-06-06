namespace mersetaWebAPI.CustomModels.Company
{
    public class SaqaQualificationResponse
    {
        public int NqfLevelId { get; set; }
        public bool WorkplaceApprovalRequired { get; set; }
        public long? QualificationId { get; set; }
        public string? QualificationTitle { get; set; }
        public string? OfoCodeDescription { get; set; }
        public string? LearnershipCode { get; set; }
        public string? OfoCode { get; set; }
        public long? SaqaQualificationId { get; set; }
        public DateTime? LastDateForEnrolment { get; set; }
        public DateTime? LastDateForAchievement { get; set; }
        
    }
}
