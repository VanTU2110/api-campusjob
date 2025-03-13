using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class DevvnTinhthanhpho
{
    public string Matp { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Slug { get; set; }

    public virtual ICollection<Companies> Companies { get; set; } = new List<Companies>();

    public virtual ICollection<Student> Student { get; set; } = new List<Student>();
}
