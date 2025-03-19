using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PatientHistory
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public string Email { get; set; } = null!;

    public string Cellphone { get; set; } = null!;

    public string? WhatsAppPhone { get; set; }

    public Guid? NeighborhoodsId { get; set; }

    public string? Address { get; set; }

    public bool FromSo { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
