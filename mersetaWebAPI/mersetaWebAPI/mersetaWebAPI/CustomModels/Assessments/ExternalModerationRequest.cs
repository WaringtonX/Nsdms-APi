namespace mersetaWebAPI.CustomModels.Assessments
{
    public class ExternalModerationRequest
    {
        public long? AssessmentProcessBatchId { get; set; }
        public long? StageOfModerationId { get; set; }
        public int? IsLearnersEmployed { get; set; }
        public int? NumberOfLearners { get; set; }
        public int? NumberOfAssessors { get; set; }
        public long? AssessmentId { get; set; }
        public int? IssueSor { get; set; }
        public string? ExternalModerationAssessorsBehaviour { get; set; }
        public string? ExternalModerationAssessmentToolsAndDocumentation { get; set; }
        public string? ValidationFindingsAndRecommendations { get; set; }
        public DateTime? ModerationDate { get; set; }
        public long? ProviderId { get; set; }
        public long? QualityAssurorId { get; set; }
        public DateTime? QualityAssurorSignatureDate { get; set; }
        public int? HasProviderLoadedCredits { get; set; }
        public string? ProviderCreditRemarks { get; set; }
    }
}
