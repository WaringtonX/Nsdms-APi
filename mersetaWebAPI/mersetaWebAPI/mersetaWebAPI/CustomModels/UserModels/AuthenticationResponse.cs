﻿namespace mersetaWebAPI.CustomModels.UserModels
{
    public class AuthenticationResponse
    {
        public long Id { get; set; }
        public ulong? Active { get; set; }
        public string? CellNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public DateTime? EmailConfirmDate { get; set; }
        public string? FaxNumber { get; set; }
        public string FirstName { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public string LastName { get; set; } = null!;
        public string? PassportNumber { get; set; }
        public string? PhoneId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? RsaIdNumber { get; set; }
        public int? Status { get; set; }
        public string? TelNumber { get; set; }
        public string? Username { get; set; }
        public string JWToken { get; set; }
        public string RoleDescription { get; set; }
    }
}
