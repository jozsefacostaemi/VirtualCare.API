using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Disability
{
    public Guid Id { get; set; }

    public Guid MedicalRecordId { get; set; }

    public bool Extension { get; set; }

    public bool RetroactiveDisability { get; set; }

    public string? ReasonMedicalAttention { get; set; }

    public int? DaysIncapacity { get; set; }

    public DateTime? StartDisability { get; set; }

    public DateTime? EndDisability { get; set; }

    public virtual ICollection<DisabilitySend> DisabilitySends { get; set; } = new List<DisabilitySend>();

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
