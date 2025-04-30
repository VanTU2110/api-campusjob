using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class Companies
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string Matp { get; set; } = null!;

    public string Maqh { get; set; } = null!;

    public string Xaid { get; set; } = null!;

    public virtual ICollection<Conversations> Conversations { get; set; } = new List<Conversations>();

    public virtual ICollection<Job> Job { get; set; } = new List<Job>();

    public virtual DevvnQuanhuyen MaqhNavigation { get; set; } = null!;

    public virtual DevvnTinhthanhpho MatpNavigation { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;

    public virtual DevvnXaphuongthitran Xa { get; set; } = null!;
}
