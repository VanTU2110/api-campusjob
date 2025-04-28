using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetCVByStudenUuidRequest: DpsParamBase
    {
        public string Student_Uuid { get; set; }
    }
}
