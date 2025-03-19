using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BusinessLineSubmenu
{
    public Guid Id { get; set; }

    public Guid BusinessLineId { get; set; }

    public Guid SubmenuId { get; set; }

    public int Order { get; set; }

    public bool Required { get; set; }

    public virtual BusinessLine BusinessLine { get; set; } = null!;

    public virtual Submenu Submenu { get; set; } = null!;
}
