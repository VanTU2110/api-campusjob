using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetAvailabilityByStudenUuid:DpsParamBase
    {
        public string studentUuid { get; set; }
    }
}
