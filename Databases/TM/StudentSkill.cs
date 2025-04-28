using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class StudentSkill
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string StudentUuid { get; set; } = null!;

    public string SkillUuid { get; set; } = null!;

    public string Proficiency { get; set; } = null!;

    /// <summary>
    /// 0: hoạt động, 1 khóa
    /// </summary>
    public sbyte Status { get; set; }

    public virtual Skills SkillUu { get; set; } = null!;

    public virtual Student StudentUu { get; set; } = null!;
}
