using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class CheckAppliedRequest:DpsParamBase
    {
        public string studentUuid {  get; set; }
        public string jobUuid {  get; set; }
    }
}
