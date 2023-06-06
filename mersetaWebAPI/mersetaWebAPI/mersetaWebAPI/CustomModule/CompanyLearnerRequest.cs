namespace mersetaWebAPI.CustomModule
{
    public class CompanyLearnerRequest
    {
        public DateTime? CreateDate { get; set; }
        public long? CompanyId { get; set; }
        public long? UserId { get; set; }
        public int? ContractOfEmploymentCopy { get; set; }
        public int? EmployedByEmployerBefore { get; set; }
        public int? WorkplaceBasedBefore { get; set; }
        public int? HighestEducationEnum { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? LastSchoolYear { get; set; }
        public long? PreviousSchools { get; set; }
        public long? TvetFetQualificationId { get; set; }
        public long? OfoCodesId { get; set; }
        public int? EmploymentStatus { get; set; }
        public int? TvetLecturer { get; set; }
        public long? SocioeconomicStatusCodeId { get; set; }
    }
}
