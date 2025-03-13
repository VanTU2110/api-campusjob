using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class Sessions
{
    public long Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

    public string? Ip { get; set; }

    public DateTime TimeLogin { get; set; }

    public DateTime? TimeLogout { get; set; }

    /// <summary>
    /// 0: Login ,1:Logout
    /// </summary>
    public sbyte? Status { get; set; }

    public virtual User UserUu { get; set; } = null!;
}
