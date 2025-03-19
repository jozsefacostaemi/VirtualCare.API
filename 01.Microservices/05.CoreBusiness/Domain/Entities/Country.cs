﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Country
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<BusinessLineLevelValueQueueConfig> BusinessLineLevelValueQueueConfigs { get; set; } = new List<BusinessLineLevelValueQueueConfig>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
