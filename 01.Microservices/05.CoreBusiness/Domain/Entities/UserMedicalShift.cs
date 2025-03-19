using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserMedicalShift
{
    public Guid Id { get; set; }

    public Guid MedicalShiftId { get; set; }

    public Guid UserId { get; set; }

    public virtual MedicalShift MedicalShift { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
