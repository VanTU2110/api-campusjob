using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class Applications
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string StudentUuid { get; set; } = null!;

    public string JobUuid { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? CoverLeter { get; set; }

    public string? Note { get; set; }

    public DateTime? AppliedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// 1: active 0: lock
    /// </summary>
    public bool? IsActive { get; set; }

    public virtual Job JobUu { get; set; } = null!;

    public virtual Student StudentUu { get; set; } = null!;
}
