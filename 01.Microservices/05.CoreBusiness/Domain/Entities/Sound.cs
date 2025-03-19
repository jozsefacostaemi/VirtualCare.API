using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Sound
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Sound1 { get; set; } = null!;

    public virtual ICollection<NotificationsDtl> NotificationsDtls { get; set; } = new List<NotificationsDtl>();
}
