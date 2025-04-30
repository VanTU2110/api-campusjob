using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetConversationListByCompanyRequest:DpsParamBase
    {
        public string CompanyUuid { get; set; } = null!;
    }
}
