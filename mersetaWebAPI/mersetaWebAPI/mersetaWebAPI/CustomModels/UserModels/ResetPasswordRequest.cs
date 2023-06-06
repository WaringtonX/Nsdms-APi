using System.ComponentModel.DataAnnotations;

namespace mersetaWebAPI.CustomModels.UserModels
{
    public class ResetPasswordRequest
    {
        [Required]
        [MinLength(6)]
        public string OldPassword { get; set; }
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
        public string OTP { get; set; }
        public string DeviceId { get; set; }
    }
}
