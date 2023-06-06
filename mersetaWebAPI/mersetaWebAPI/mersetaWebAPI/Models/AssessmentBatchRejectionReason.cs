namespace mersetaWebAPI.Models
{
    public class AssessmentBatchRejectionReason
    {
        public long AssessmentBatchRejectionReasonId { get; set; }
        public long RejectionReasonId { get; set; }
        public long AssessmentProcessBatchId { get; set; }
    }
}
