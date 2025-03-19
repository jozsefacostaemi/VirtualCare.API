using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParaclinicalHed
{
    public Guid Id { get; set; }

    public Guid MedicalRecordId { get; set; }

    public string Recomendations { get; set; } = null!;

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual ICollection<ParaclinicalDtl> ParaclinicalDtls { get; set; } = new List<ParaclinicalDtl>();

    public virtual ICollection<ParaclinicalSend> ParaclinicalSends { get; set; } = new List<ParaclinicalSend>();
}
