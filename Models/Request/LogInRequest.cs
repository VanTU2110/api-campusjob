using System.ComponentModel.DataAnnotations;
using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class LogInRequest : DpsParamBase
    {
       
        public string Email { get; set; }
        public string Password { get; set; }


/*        public string? FCMToken { get; set; }*/
    }
}
