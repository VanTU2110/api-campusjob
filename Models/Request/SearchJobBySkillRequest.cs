namespace apicampusjob.Models.Request
{
    public class SearchJobBySkillRequest:BaseKeywordPageRequest
    {
        public string skillUuid { get; set; }
    }
}
