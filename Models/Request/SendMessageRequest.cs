using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class SendMessageRequest:DpsParamBase
    {
        public string ConversationUuid { get; set; }
        public string SenderUuid { get; set; }
        public string Content { get; set; }
    }
}
