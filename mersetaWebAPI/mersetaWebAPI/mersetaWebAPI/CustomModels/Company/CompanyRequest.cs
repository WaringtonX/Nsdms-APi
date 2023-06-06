namespace mersetaWebAPI.CustomModels.Company
{
    public class CompanyRequest
    {
        public long Id { get; set; }
        public string? CompanyGuid { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyRegistrationNumber { get; set; }
        public int? CompanyStatus { get; set; }
        public int? CompanyType { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Email { get; set; }
        public string? FaxNumber { get; set; }
        public string? LevyNumber { get; set; }
        public string? LevyNumberAdditional { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string? PayeSdlNumber { get; set; }
        public string? TelNumber { get; set; }
        public string? TradingName { get; set; }
    }
}
