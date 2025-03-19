using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PatientStateHistory
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientStateId { get; set; }

    public string? Comments { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual PatientState PatientState { get; set; } = null!;
}
