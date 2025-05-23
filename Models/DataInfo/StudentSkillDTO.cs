namespace apicampusjob.Models.DataInfo
{
    public class StudentSkillDTO:BaseDTO
    {
        public string StudentUuid { get; set; }
        public SkillDTO Skill { get; set; }
        public string Proficiency {  get; set; }
    }
}
