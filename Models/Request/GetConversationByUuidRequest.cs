using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetConversationByUuidRequest:DpsParamBase
    {
        public string Uuid { get; set; } = null!;
    }
}
