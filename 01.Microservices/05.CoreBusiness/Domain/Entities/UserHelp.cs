using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserHelp
{
    public Guid Id { get; set; }

    public Guid HelpId { get; set; }

    public Guid UserId { get; set; }

    public string Comments { get; set; } = null!;

    public bool? Resolve { get; set; }

    public string? ResolveComments { get; set; }

    public DateTime? ResolveAt { get; set; }

    public virtual Help Help { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
