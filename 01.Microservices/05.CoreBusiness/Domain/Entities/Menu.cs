using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Menu
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Order { get; set; }

    public string Code { get; set; } = null!;

    public virtual ICollection<Submenu> Submenus { get; set; } = new List<Submenu>();
}
