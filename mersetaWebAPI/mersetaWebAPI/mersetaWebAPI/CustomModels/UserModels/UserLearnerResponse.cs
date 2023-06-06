namespace mersetaWebAPI.CustomModels.UserModels
{
    public class UserLearnerResponse
    {
        public long Id { get; set; }
        public string? LastName { get; set; } 
        public string? FirstName { get; set; }
        public long? Title { get; set; }
        public string? MiddleName { get; set; }
        public string? MaidenName { get; set; }
        public string? CellNumber { get; set; }
        public string? TelNumber { get; set; }
        public string? PrimaryEmailAddress { get; set; }
        public string? SecondaryEmailAddress { get; set; }
        public long? MaritalStatus { get; set; }
        public string? DateOfBirth { get; set; }
        public long? Gender { get; set; }
        public long? Equity { get; set; }
        public long? CompanyLearnerId { get; set; }
        public long? Disability { get; set; }
        public bool? AcceptPopi { get; set; }
        public DateTime? AcceptPopiDate { get; set; }
        public string? PassportNumber { get; set; }
        public int? DisabilityStatus { get; set; }
        public long? LearnershipId { get; set; }
        public DateTime? LastSchoolYear { get; set; }
        public int? CitizenResidentStatus { get; set; }
        public string? AlternativeIdNumber { get; set; }
        public long? AlternativeIdType { get; set; }
        public long? PostalAddressId { get; set; }
        public long? ResidentialAddressId { get; set; }
        public long? UrbanRuralEnum { get; set; }
        public long? EmploymentStatus { get; set; }
        public long? PreviousSchools { get; set; }
        public long? QualificationId { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinTelephone { get; set; }


    }
}
