using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Help
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<UserHelp> UserHelps { get; set; } = new List<UserHelp>();
}
