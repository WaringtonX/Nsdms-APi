namespace mersetaWebAPI.CustomModels.Company
{
    public class CompanyResponse
    {
        public long Id { get; set; }
        public string? CompanyGuid { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyRegistrationNumber { get; set; }
        public int? CompanyStatus { get; set; }
        public int CompanyType { get; set; }
        public string? LevyNumber { get; set; }
        public string? TradingName { get; set; }
        public string? AccreditationNumber { get; set; }
        public string? levy_number_additional { get; set; }
    }
}
