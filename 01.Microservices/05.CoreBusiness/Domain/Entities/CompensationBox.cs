using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CompensationBox
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int? Score { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
