using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserHistory
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public bool? LogIn { get; set; }

    public bool? LogOut { get; set; }

    public virtual User User { get; set; } = null!;
}
