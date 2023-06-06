namespace mersetaWebAPI.CustomModels.UserModels
{
    public class AuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
    }
}
