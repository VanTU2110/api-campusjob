using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetCVByStudenUuidRequest: DpsParamBase
    {
        public string studentUuid { get; set; }
    }
}
