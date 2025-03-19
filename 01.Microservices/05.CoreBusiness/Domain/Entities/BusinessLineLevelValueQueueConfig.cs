using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BusinessLineLevelValueQueueConfig
{
    public Guid Id { get; set; }

    public Guid LevelQueueId { get; set; }

    public Guid? CountryId { get; set; }

    public Guid? DepartmentId { get; set; }

    public Guid? CityId { get; set; }

    public Guid ServiceId { get; set; }

    public Guid BusinessLineId { get; set; }

    public virtual BusinessLine BusinessLine { get; set; } = null!;

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Department? Department { get; set; }

    public virtual LevelQueue LevelQueue { get; set; } = null!;

    public virtual ICollection<QueueConf> QueueConfs { get; set; } = new List<QueueConf>();

    public virtual Service Service { get; set; } = null!;
}
