using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class HealthPolicy
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public Guid? HealthEntityId { get; set; }

    public virtual HealthEntity? HealthEntity { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
