using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Plan
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int Number { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
