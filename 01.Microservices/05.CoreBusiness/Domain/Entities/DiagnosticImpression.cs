using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DiagnosticImpression
{
    public Guid Id { get; set; }

    public Guid DiagnosticId { get; set; }

    public Guid DiagnosticRelatedId { get; set; }

    public string? MedicalConcept { get; set; }

    public string? Recomendations { get; set; }

    public Guid MedicalRecordId { get; set; }

    public virtual Diagnostic Diagnostic { get; set; } = null!;

    public virtual Diagnostic DiagnosticRelated { get; set; } = null!;

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
