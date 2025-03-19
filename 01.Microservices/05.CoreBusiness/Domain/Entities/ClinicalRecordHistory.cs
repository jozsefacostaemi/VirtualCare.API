using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ClinicalRecordHistory
{
    public Guid? Id { get; set; }

    public Guid? ClinicalRecordTypeId { get; set; }

    public string? Value { get; set; }

    public int? Order { get; set; }

    public Guid MedicalRecordId { get; set; }

    public bool? Important { get; set; }

    public bool FromSo { get; set; }

    public virtual ClinicalRecordHistoriesType? ClinicalRecordType { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
