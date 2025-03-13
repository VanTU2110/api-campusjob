using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class BaseKeywordPageRequest:DpsPagingParamBase
    {
        public string? Keyword { get; set; } 

        public sbyte? Status { get; set; }
    }
}
