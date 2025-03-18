using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class JobSchedule
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string JobUuid { get; set; } = null!;

    public string DayOfWeek { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual Job JobUu { get; set; } = null!;
}
