namespace mersetaWebAPI.CustomModels.Provider
{
    public class TraininingInformationRequest
    {
        public int? PivotNonPivot { get; set; }
        public DateTime? CommencementDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? MersetaFunded { get; set; }
        public long? InterventionTypeId { get; set; }
        public long? NqfAlignedId { get; set; }
    }
    
}
