using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserStateHistory
{
    public Guid Id { get; set; }

    public Guid UserStateId { get; set; }

    public Guid UserId { get; set; }

    public string? Comments { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual UserState UserState { get; set; } = null!;
}
