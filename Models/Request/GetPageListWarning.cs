using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetPageListWarning:DpsPagingParamBase
    {
        public string? TargetType { get; set; }
        public string? TargetUuid { get; set;} 
    }
}
