using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetPageListApplyByStudentUuid:BaseKeywordPageRequest
    {
        public string StudentUuid { get; set; }
    }
}
