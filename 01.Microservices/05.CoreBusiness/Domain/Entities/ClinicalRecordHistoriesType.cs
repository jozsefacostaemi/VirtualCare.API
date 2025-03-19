using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ClinicalRecordHistoriesType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int Order { get; set; }
}
