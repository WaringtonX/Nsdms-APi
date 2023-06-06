namespace mersetaWebAPI.CustomModule
{
    public class AddressRequestt
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? MunicipalityId { get; set; }
        public long? UserId { get; set; }
        public long? TownId { get; set; }
        public long? CompanyId { get; set; }
        public string? Postcode { get; set; }
        public long? StatsSaareaCodeId { get; set; }
        public ulong? SameAddress { get; set; }

    }

}
