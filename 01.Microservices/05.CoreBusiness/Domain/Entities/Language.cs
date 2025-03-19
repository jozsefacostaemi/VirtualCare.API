using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Language
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<BusinessLine> BusinessLines { get; set; } = new List<BusinessLine>();

    public virtual ICollection<Translation> Translations { get; set; } = new List<Translation>();
}
