using System;
using System.Collections.Generic;

namespace Auth.VirtualCare.API;

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
    public virtual UserState UserState { get; set; } = null!;
}
