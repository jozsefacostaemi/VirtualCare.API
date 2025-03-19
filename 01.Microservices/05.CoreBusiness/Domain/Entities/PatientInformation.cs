using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PatientInformation
{
    public Guid Id { get; set; }

    public string? ReasonConsultations { get; set; }

    public string? ClinicalPresentation { get; set; }

    public string CurrentIllness { get; set; } = null!;

    public Guid MedicalRecordId { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
