namespace mersetaWebAPI.CustomModule
{
    public class UpdateCompanyLearner
    {
        public long? LearnershipId { get; set; }
        public long? InterventionTypeId { get; set; }
        public int? PivotNonPivot { get; set; }
        public long? NqfAlignedId { get; set; }
        public long? QualificationId { get; set; }
        public DateTime? CommencementDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? MersetaFunded { get; set; }
        public int? LearnerStatus { get; set; }
        public long? DundingId { get; set; }
    }
}
