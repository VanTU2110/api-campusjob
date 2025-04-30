using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class Conversations
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string StudentUuid { get; set; } = null!;

    public string CompanyUuid { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Companies CompanyUu { get; set; } = null!;

    public virtual ICollection<Messages> Messages { get; set; } = new List<Messages>();

    public virtual Student StudentUu { get; set; } = null!;
}
