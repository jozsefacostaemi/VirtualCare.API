using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Color
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string ColorHex { get; set; } = null!;

    public virtual ICollection<NotificationsDtl> NotificationsDtls { get; set; } = new List<NotificationsDtl>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
