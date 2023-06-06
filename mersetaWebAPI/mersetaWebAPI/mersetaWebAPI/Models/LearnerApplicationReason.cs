namespace mersetaWebAPI.Models
{
    public class LearnerApplicationReason
    {
        public long Id { get; set; }
        public long LearnerLifecycleId { get; set; }
        public long? TransferReason { get; set; }
        public long? RejectionReseanId { get; set; }
        public long? WithdrawalReasonId { get; set; }
    }
}
