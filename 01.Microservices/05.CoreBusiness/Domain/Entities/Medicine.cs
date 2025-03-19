using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Medicine
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<PrescriptionsDtl> PrescriptionsDtls { get; set; } = new List<PrescriptionsDtl>();
}
