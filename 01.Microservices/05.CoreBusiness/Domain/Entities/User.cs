using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public Guid UserIdSo { get; set; }

    public bool Loggued { get; set; }

    public Guid UserStateId { get; set; }

    public Guid UserExpireId { get; set; }

    public byte[]? Photo { get; set; }

    public int AutoNumber { get; set; }

    public Guid? MedicalShiftId { get; set; }

    public Guid BusinessLineId { get; set; }

    public string? Name { get; set; }

    public DateTime? AvailableAt { get; set; }

    public Guid CityId { get; set; }

    public string? Token { get; set; }

    public DateTime? TokenExpiryDate { get; set; }

    public string UserName { get; set; } = null!;

    public string? Password { get; set; }

    public virtual BusinessLine BusinessLine { get; set; } = null!;

    public virtual ICollection<BusinessLine> BusinessLines { get; set; } = new List<BusinessLine>();

    public virtual City City { get; set; } = null!;

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual UserExpire UserExpire { get; set; } = null!;

    public virtual ICollection<UserHelp> UserHelps { get; set; } = new List<UserHelp>();

    public virtual ICollection<UserHistory> UserHistories { get; set; } = new List<UserHistory>();

    public virtual ICollection<UserMedicalShift> UserMedicalShifts { get; set; } = new List<UserMedicalShift>();

    public virtual ICollection<UserService> UserServices { get; set; } = new List<UserService>();

    public virtual UserState UserState { get; set; } = null!;

    public virtual ICollection<UserStateHistory> UserStateHistories { get; set; } = new List<UserStateHistory>();
}
