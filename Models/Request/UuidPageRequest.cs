using apicampusjob.Models.Request;

namespace apicampusjob.Models.Request
{
    public class UuidPageRequest : BaseKeywordPageRequest
    {
        public string? Uuid { get; set; }
    }
}