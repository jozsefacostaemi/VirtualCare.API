using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PatientState
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<PatientStateHistory> PatientStateHistories { get; set; } = new List<PatientStateHistory>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
