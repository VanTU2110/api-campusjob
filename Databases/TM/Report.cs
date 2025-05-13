using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class Report
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string ReporterUuid { get; set; } = null!;

    public string TargetType { get; set; } = null!;

    public string TargetUuid { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
