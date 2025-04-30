using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class Messages
{
    public int Id { get; set; }

    public string ConversationUuid { get; set; } = null!;

    public string Uuid { get; set; } = null!;

    public string SenderUuid { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime SendAt { get; set; }

    public virtual Conversations ConversationUu { get; set; } = null!;
}
