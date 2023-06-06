using System.ComponentModel.DataAnnotations;

namespace mersetaWebAPI.CustomModels.UserModels
{
    public class ResentOTPRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string DeviceId { get; set; }
    }
}
