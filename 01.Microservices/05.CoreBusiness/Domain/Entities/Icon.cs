using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Icon
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public byte[] Value { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual ICollection<Triage> Triages { get; set; } = new List<Triage>();
}
