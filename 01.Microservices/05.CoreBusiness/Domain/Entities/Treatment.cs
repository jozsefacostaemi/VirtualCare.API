using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Treatment
{
    public Guid Id { get; set; }

    public string MedicationName { get; set; } = null!;

    public int MedicationDose { get; set; }

    public int Order { get; set; }

    public Guid MedicalRecordId { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
