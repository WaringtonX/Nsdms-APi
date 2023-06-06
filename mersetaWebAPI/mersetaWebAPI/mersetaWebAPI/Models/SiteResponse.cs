namespace mersetaWebAPI.Models
{
    public class SiteResponse
    {

        public long Id { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? FaxNumber { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string? TelNumber { get; set; }
        public long? CompanyId { get; set; }
        public long? FormUserId { get; set; }
        public long? RegisteredAddressId { get; set; }
        public int? SiteStatus { get; set; }
        public string? CompanySiteNumber { get; set; }
    }
}
