using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MedicalRecordHistory
{
    public Guid Id { get; set; }

    public Guid MedicalRecordId { get; set; }

    public Guid MedicalRecordStateId { get; set; }

    public string? Comments { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual MedicalRecordState MedicalRecordState { get; set; } = null!;
}
