using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserExpire
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int MinutesExpires { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
