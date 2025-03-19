using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ClinicalRecordGynObstHistory
{
    public Guid Id { get; set; }

    public int Abortions { get; set; }

    public int? Caesareans { get; set; }

    public int? Gravidas { get; set; }

    public int? Childbirths { get; set; }

    public DateTime? Fpp { get; set; }

    public DateTime? Fum { get; set; }

    public Guid MedicalRecordId { get; set; }

    public bool? IsPregnancy { get; set; }

    public bool FromSo { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
