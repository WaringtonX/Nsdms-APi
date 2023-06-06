using System.ComponentModel.DataAnnotations;

namespace mersetaWebAPI.CustomModels.EmailModels
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string VerificationOTP { get; set; }
        public string DeviceId { get; set; }
    }
}
