using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MedicalShift
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<UserMedicalShift> UserMedicalShifts { get; set; } = new List<UserMedicalShift>();
}
