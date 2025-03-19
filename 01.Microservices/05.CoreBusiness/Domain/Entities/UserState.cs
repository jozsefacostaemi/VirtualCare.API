using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserState
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int Order { get; set; }

    public bool IsBreak { get; set; }

    public bool RequiredComment { get; set; }

    public virtual ICollection<UserStateHistory> UserStateHistories { get; set; } = new List<UserStateHistory>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
