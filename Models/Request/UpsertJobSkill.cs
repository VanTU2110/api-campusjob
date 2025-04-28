using System.ComponentModel.DataAnnotations;

namespace apicampusjob.Models.Request
{
    public class UpsertJobSkill:UuidRequest
    {
        [Required(ErrorMessage = "JobUuid is required")]
        public string JobUuid { get; set; } = null!;
        [Required(ErrorMessage = "SkillUuid is required")]

        public string SkillUuid { get; set; } = null!;
    }
}
