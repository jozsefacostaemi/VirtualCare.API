using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PrescriptionsDtl
{
    public Guid Id { get; set; }

    public Guid MedicineId { get; set; }

    public Guid AdministrationRouteId { get; set; }

    public Guid PresentationId { get; set; }

    public Guid DosageId { get; set; }

    public int Quantity { get; set; }

    public bool ScaleHours { get; set; }

    public bool ScaleDays { get; set; }

    public int ScaleValue { get; set; }

    public bool FrecuencyHours { get; set; }

    public bool FrecuencyDays { get; set; }

    public int FrecuencyValue { get; set; }

    public string? Recomendations { get; set; }

    public Guid PrescriptionHed { get; set; }

    public virtual AdministrationRoute AdministrationRoute { get; set; } = null!;

    public virtual Dosage Dosage { get; set; } = null!;

    public virtual Medicine Medicine { get; set; } = null!;

    public virtual PrescriptionHed PrescriptionHedNavigation { get; set; } = null!;

    public virtual Presentation Presentation { get; set; } = null!;
}
