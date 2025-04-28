using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class StudentCv
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string StudentUuid { get; set; } = null!;

    public string CloudinaryPublicId { get; set; } = null!;

    public string Url { get; set; } = null!;

    public DateTime UploadAt { get; set; }

    public virtual Student StudentUu { get; set; } = null!;
}
