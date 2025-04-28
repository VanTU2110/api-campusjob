using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class GetListStudentSkillByStudentUuid:DpsParamBase
    {
        public string Student_Uuid { get; set; }
    }
}
