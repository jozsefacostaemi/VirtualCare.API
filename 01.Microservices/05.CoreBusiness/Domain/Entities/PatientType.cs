using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PatientType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Code { get; set; } = null!;
}
