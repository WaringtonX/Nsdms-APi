namespace mersetaWebAPI.CustomModule
{
    public class AssessorResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? RsaIdNumber { get; set; }
        public long UserId { get; set; }
        public long UsersId { get; set; }
        public long QualificationId { get; set; }
        public long AssessModAppId { get; set; }
    }
}
