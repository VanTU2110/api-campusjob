using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class JobSkill
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string JobUuid { get; set; } = null!;

    public string SkillUuid { get; set; } = null!;

    public virtual Job JobUu { get; set; } = null!;

    public virtual Skills SkillUu { get; set; } = null!;
}
