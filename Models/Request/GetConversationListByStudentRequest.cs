using apicampusjob.Models.BaseRequest;
using System.Data.Common;

namespace apicampusjob.Models.Request
{
    public class GetConversationListByStudentRequest:DpsParamBase
    {
        public string StudentUuid { get; set; } = null!;
    }
}
