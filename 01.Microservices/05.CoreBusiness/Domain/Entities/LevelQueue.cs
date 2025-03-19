using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class LevelQueue
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<BusinessLineLevelValueQueueConfig> BusinessLineLevelValueQueueConfigs { get; set; } = new List<BusinessLineLevelValueQueueConfig>();

    public virtual ICollection<BusinessLine> BusinessLines { get; set; } = new List<BusinessLine>();
}
