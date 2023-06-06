namespace mersetaWebAPI.Models
{
    public class AssessmentProcessBatch
    {
        public long AssessmentProcessBatchId { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string? Comments { get; set; }   
        public long ProcessStatusId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public long CompanyId { get; set; }
    }
}
