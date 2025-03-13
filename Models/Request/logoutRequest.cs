using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class logoutRequest: DpsParamBase
    {
        public string token { get; set; }
    }
}
