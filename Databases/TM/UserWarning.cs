using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class UserWarning
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string TargetType { get; set; } = null!;

    public string TargetUuid { get; set; } = null!;

    public string Messages { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
