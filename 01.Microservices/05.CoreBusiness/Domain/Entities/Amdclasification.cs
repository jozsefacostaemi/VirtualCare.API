using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Amdclasification
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<Amd> Amds { get; set; } = new List<Amd>();
}
