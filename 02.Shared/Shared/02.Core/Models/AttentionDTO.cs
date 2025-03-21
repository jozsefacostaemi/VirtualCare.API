using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClasses._02.Core.DTOs;

public class AttentionDTO
{
    public Guid AttentionId { get; set; }
    public int? Priority { get; set; }
    public string? User { get; set; }
    public required string Patient { get; set; }
    public required string Process { get; set; }
    public required string City { get; set; }
    public string? PatientNum { get; set; }
    public int? Comorbidities { get; set; }
    public required string Age { get; set; }
    public required string State { get; set; }
    public required string Plan { get; set; }
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
    public Guid? PatientId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? CityId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? CountryId { get; set; }
    public Guid? ProcessId { get; set; }
    public Guid? MedicalRecordStateId { get; set; }
    public required string ProcessCode { get; set; }
}

