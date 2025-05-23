namespace apicampusjob.Models.Request
{
    public class UpsertStudentSkillRequest:UuidRequest
    {
        public string studentUuid { get; set; }
        public string skillUuid { get; set; }
        public string Proficiency { get; set; }
    }
}
