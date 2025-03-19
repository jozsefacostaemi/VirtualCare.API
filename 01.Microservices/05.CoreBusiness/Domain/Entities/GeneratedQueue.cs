﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class GeneratedQueue
{
    public Guid Id { get; set; }

    public Guid QueueConfId { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual QueueConf QueueConf { get; set; } = null!;
}
