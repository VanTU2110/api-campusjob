using apicampusjob.Models.BaseRequest;
using System.ComponentModel.DataAnnotations;

namespace apicampusjob.Models.Request
{
    public class VerifyUserRequest:DpsParamBase
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Otp { get; set; }
    }
}
