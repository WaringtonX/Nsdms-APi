namespace mersetaWebAPI.CustomModels.Provider
{
    public class ProviderResponse
    {
        public long? Id { get; set; }
        public string? CompanyGuid { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyRegistrationNumber { get; set; }
        public int? CompanyStatus { get; set; }
        public int? CompanyType { get; set; }
        public string? LevyNumber { get; set; }
        public string? TradingName { get; set; }
        public string? AccreditationNumber { get; set; }
        public string? levy_number_additional { get; set; }
        public long? SicCodeId { get; set; }
        public int? SetaId { get; set; }
        public int? AccreditationApplicationType { get; set; }
        public int? AccreditationStatus { get; set; }
        public DateTime? EtqaReviewCommitteeDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? FaxNumber { get; set; }  
        public string? TelNumber { get; set; }
        public string? CellNumber { get; set; } 
    }
}
