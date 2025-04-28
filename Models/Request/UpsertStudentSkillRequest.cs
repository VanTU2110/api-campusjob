namespace apicampusjob.Models.Request
{
    public class UpsertStudentSkillRequest:UuidRequest
    {
        public string Student_Uuid { get; set; }
        public string Skill_Uuid { get; set; }
        public string Proficiency { get; set; }
    }
}
