using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserService
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ServiceId { get; set; }

    public bool ServicePriority { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
