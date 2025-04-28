using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetPageListApplyByJobUuid:BaseKeywordPageRequest
    {
        public string JobUuid { get; set; }
    }
}
