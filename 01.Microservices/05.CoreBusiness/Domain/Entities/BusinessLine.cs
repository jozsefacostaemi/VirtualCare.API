using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BusinessLine
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public byte[]? LogoId { get; set; }

    public Guid LanguageId { get; set; }

    public Guid CityId { get; set; }

    public string Address { get; set; } = null!;

    public Guid? ExperienceCenterLeaderId { get; set; }

    public Guid LevelQueueId { get; set; }

    public virtual ICollection<BusinessLineLevelValueQueueConfig> BusinessLineLevelValueQueueConfigs { get; set; } = new List<BusinessLineLevelValueQueueConfig>();

    public virtual ICollection<BusinessLineSubmenu> BusinessLineSubmenus { get; set; } = new List<BusinessLineSubmenu>();

    public virtual User? ExperienceCenterLeader { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual LevelQueue LevelQueue { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
