using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Diagnostic
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string CodeCie10 { get; set; } = null!;

    public virtual ICollection<DiagnosticImpression> DiagnosticImpressionDiagnosticRelateds { get; set; } = new List<DiagnosticImpression>();

    public virtual ICollection<DiagnosticImpression> DiagnosticImpressionDiagnostics { get; set; } = new List<DiagnosticImpression>();
}
