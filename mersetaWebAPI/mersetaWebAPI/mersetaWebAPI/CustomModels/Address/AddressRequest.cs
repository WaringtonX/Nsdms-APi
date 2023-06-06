namespace mersetaWebAPI.CustomModels.Address
{
    public class AddressRequest
    {
        public long Id { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? AddressLine4 { get; set; }
        public DateTime? CreateDate { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Postcode { get; set; }
        public ulong? PrimaryAddr { get; set; }
        public ulong? SameAddress { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? MunicipalityId { get; set; }
        public long? UserId { get; set; }
        public long? SitesId { get; set; }
        public long? TownId { get; set; }
    }
}
