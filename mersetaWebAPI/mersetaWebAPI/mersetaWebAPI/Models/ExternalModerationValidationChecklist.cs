namespace mersetaWebAPI.Models
{
    public class ExternalModerationValidationChecklist
    {
        public long ExternalModerationValidationChecklistId { get; set; }
        public long? ExternalModerationValidationId { get; set; }
        public long? ModerationQuestionId { get; set; }
        public int? Answer { get; set; }
        public string? EvidenceComments { get; set; }

    }
}
