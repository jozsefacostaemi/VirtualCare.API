using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class City
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public bool Active { get; set; }

    public Guid DepartmentId { get; set; }

    public virtual ICollection<BusinessLineLevelValueQueueConfig> BusinessLineLevelValueQueueConfigs { get; set; } = new List<BusinessLineLevelValueQueueConfig>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
