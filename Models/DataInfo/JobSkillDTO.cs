namespace apicampusjob.Models.DataInfo
{
    public class JobSkillDTO:BaseDTO
    {
        public string JobUuid { get; set; }
        public SkillDTO Skill { get; set; }
    }
}
