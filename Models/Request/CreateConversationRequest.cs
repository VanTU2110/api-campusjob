using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class CreateConversationRequest:DpsParamBase
    {
        public string StudentUuid { get; set; } = null!;
        public string CompanyUuid { get; set; } = null!;
    }
}
