using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetListStudentSkillByStudentUuid:DpsParamBase
    {
        public string studentUuid { get; set; }
    }
}
