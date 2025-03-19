using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MedicalRecordState
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<MedicalRecordHistory> MedicalRecordHistories { get; set; } = new List<MedicalRecordHistory>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
