using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Service
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public Guid? ColorId { get; set; }

    public Guid? IconId { get; set; }

    public int AutoNumber { get; set; }

    public int NextMedicalRecordWaitTimeSecond { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? ModalityId { get; set; }

    public Guid? NotificationHedId { get; set; }

    public virtual ICollection<BusinessLineLevelValueQueueConfig> BusinessLineLevelValueQueueConfigs { get; set; } = new List<BusinessLineLevelValueQueueConfig>();

    public virtual Color? Color { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Icon? Icon { get; set; }

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual Modality? Modality { get; set; }

    public virtual NotificationsHead? NotificationHed { get; set; }

    public virtual ICollection<UserService> UserServices { get; set; } = new List<UserService>();
}
