using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Amd
{
    public Guid Id { get; set; }

    public Guid MedicalRecordId { get; set; }

    public Guid AmdclasificationId { get; set; }

    public string Email { get; set; } = null!;

    public string CellPhone { get; set; } = null!;

    public string? Phone { get; set; }

    public Guid NeighborhoodId { get; set; }

    public string Address { get; set; } = null!;

    public string? AdrressReference { get; set; }

    public decimal? Copay { get; set; }

    public long? WaitingTime { get; set; }

    public bool? HasCoverage { get; set; }

    public bool? ConfirmInfoAmd { get; set; }

    public virtual Amdclasification Amdclasification { get; set; } = null!;

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual Neighborhood Neighborhood { get; set; } = null!;
}
