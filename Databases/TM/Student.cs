using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class Student
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

    public string? Fullname { get; set; }

    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 0-Nam , 1-Nữ , 2 - khác
    /// </summary>
    public sbyte Gender { get; set; }

    public DateOnly Birthday { get; set; }

    public string? University { get; set; }

    public string? Major { get; set; }

    public string Matp { get; set; } = null!;

    public string Maqh { get; set; } = null!;

    public string Xaid { get; set; } = null!;

    public virtual ICollection<Applications> Applications { get; set; } = new List<Applications>();

    public virtual DevvnQuanhuyen MaqhNavigation { get; set; } = null!;

    public virtual DevvnTinhthanhpho MatpNavigation { get; set; } = null!;

    public virtual ICollection<StudentAvailability> StudentAvailability { get; set; } = new List<StudentAvailability>();

    public virtual ICollection<StudentCv> StudentCv { get; set; } = new List<StudentCv>();

    public virtual ICollection<StudentSkill> StudentSkill { get; set; } = new List<StudentSkill>();

    public virtual User UserUu { get; set; } = null!;

    public virtual DevvnXaphuongthitran Xa { get; set; } = null!;
}
