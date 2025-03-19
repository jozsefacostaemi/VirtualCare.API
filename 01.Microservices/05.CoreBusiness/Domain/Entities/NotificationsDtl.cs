using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class NotificationsDtl
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Code { get; set; } = null!;

    public long Seconds { get; set; }

    public byte[] Sound { get; set; } = null!;

    public string HexColor { get; set; } = null!;

    public Guid NotificationHedId { get; set; }

    public Guid? SoundId { get; set; }

    public Guid? ColorId { get; set; }

    public virtual Color? Color { get; set; }

    public virtual NotificationsHead NotificationHed { get; set; } = null!;

    public virtual Sound? SoundNavigation { get; set; }
}
