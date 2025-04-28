namespace apicampusjob.Models.DataInfo
{
    public class StudentSkillDTO:BaseDTO
    {
        public string Student_Uuid { get; set; }
        public SkillDTO Skill { get; set; }
        public string proficiency {  get; set; }
    }
}
