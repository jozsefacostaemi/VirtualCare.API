using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class HealthEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int? Score { get; set; }

    public virtual ICollection<HealthPolicy> HealthPolicies { get; set; } = new List<HealthPolicy>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
