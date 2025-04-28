using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class CancellationofApplicationRquest:DpsParamBase
    {
        public string Application_uuid { get; set; }
    }
}
