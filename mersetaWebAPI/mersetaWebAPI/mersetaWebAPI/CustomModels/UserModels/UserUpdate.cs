namespace mersetaWebAPI.CustomModels.UserModels
{
    public class UserUpdate
    {
        public long Id { get; set; }
        public string? CellNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? FaxNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string? PassportNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? RsaIdNumber { get; set; }
        public string? MaidenName { get; set; }
        public string? TelNumber { get; set; }
        public string? Username { get; set; }
        public long? TitleId { get; set; }
        public long? DisabledId { get; set; }
        public long? GenderId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string Password { get; set; } 
    }
}
