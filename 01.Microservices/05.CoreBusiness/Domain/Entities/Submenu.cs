using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Submenu
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Code { get; set; } = null!;

    public Guid MenuId { get; set; }

    public virtual ICollection<BusinessLineSubmenu> BusinessLineSubmenus { get; set; } = new List<BusinessLineSubmenu>();

    public virtual Menu Menu { get; set; } = null!;
}
