using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetMessagesByConversation:DpsParamBase
    {
        public string ConversationUuid { get; set; } = null!;
    }
}
