using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class User
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    /// <summary>
    /// 0-sinh vien 1-nha tuyen dung(cty)
    /// </summary>
    public sbyte Role { get; set; }

    /// <summary>
    /// 0-Khóa, 1-Đang hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual Companies? Companies { get; set; }

    public virtual ICollection<Sessions> Sessions { get; set; } = new List<Sessions>();

    public virtual Student? Student { get; set; }
}
