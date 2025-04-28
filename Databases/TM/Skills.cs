using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class Skills
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<JobSkill> JobSkill { get; set; } = new List<JobSkill>();

    public virtual ICollection<StudentSkill> StudentSkill { get; set; } = new List<StudentSkill>();
}
