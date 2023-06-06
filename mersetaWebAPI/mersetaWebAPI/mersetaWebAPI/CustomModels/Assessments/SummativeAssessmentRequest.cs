namespace mersetaWebAPI.CustomModels.Assessments
{
    public class SummativeAssessmentRequest
    {
       
        public DateTime? CreateDate { get; set; }
        public int? PivotNonPivot { get; set; }
        public long? CompanyLearnersId { get; set; }
       // public long? InterventionLevelId { get; set; }
        public long? InterventionTypeId { get; set; }
        public long? NonSetaQualificationsCompletionId { get; set; }
        public long? NqfAlignedId { get; set; }
        public long? NqfLevelsId { get; set; }
        public long? QualificationId { get; set; }
        public long? SkillsProgramId { get; set; }
        public long? SkillsSetId { get; set; }
        public long? TrainingProviderVerficationId { get; set; }
        public long? UnitStandardId { get; set; }
        public long? UsersId { get; set; }
       public long? AssessmentProgressStatusTypeId { get; set; }
    }
}
