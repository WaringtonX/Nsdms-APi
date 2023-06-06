namespace mersetaWebAPI.CustomModule
{
    public class RegistrationRequestt
    {
        //public ulong? Active { get; set; }
        public string? CellNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        // public DateTime? EmailConfirmDate { get; set; }
        public string? FaxNumber { get; set; }
        public string FirstName { get; set; } = null!;
        //public DateTime? LastLogin { get; set; }
        public string LastName { get; set; } = null!;
        public string? PassportNumber { get; set; }
        // public string? PhoneId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? RsaIdNumber { get; set; }
        public string Password { get; set; }
        public string? Username { get; set; }

        public string? MiddleName { get; set; }
        public long? TitleId { get; set; }
        public long? NationalityId { get; set; }
        public string? PrimaryEmailAddress { get; set; }
        public long? DisabilityStatus { get; set; }
        public long? DisabledId { get; set; }
        public long? GenderId { get; set; }
        public long? EquityId { get; set; }
        public long? DisabilityRatingId { get; set; }
        public string? AlternativeIdNumber { get; set; }
        public long? AlternativeIdType { get; set; }

    }

}
