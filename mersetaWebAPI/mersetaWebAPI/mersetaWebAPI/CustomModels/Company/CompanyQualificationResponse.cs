namespace mersetaWebAPI.CustomModels.Company
{
    public class CompanyQualificationResponse
    {
        public long Id { get; set; }
        public long? QualificationId { get; set; }
        public string? QualificationTitle { get; set; }
        public long? NqfLevelId { get; set; }
        public long? WorkplaceApprovalRequired { get; set; }
        
    }
}
