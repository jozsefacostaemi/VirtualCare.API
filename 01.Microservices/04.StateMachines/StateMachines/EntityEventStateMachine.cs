using Domain.Enums;

namespace StateMachines;
public class EntityEventStateMachine
{
    public StateEventProcessEnum EventType { get; set; }

    // ID de entidades
    public Guid? UserId { get; set; }
    public Guid? PatientId { get; set; }
    public Guid? MedicalRecordId { get; set; }

    // Código de entidades actuales
    public PatientStateEnum? ActualPatientStateCode { get; set; }
    public UserStateEnum? ActualUserStateCode { get; set; }
    public MedicalRecordStateEnum? ActualMedicalRecordStateCode { get; set; }


    // Nuevos códigos de entidades 
    public PatientStateEnum? NewPatientStateCode { get; set; }
    public UserStateEnum? NewUserStateCode { get; set; }
    public MedicalRecordStateEnum? NewMedicalRecordStateCode { get; set; }


    // ID de nuevos estados 
    public Guid NewPatientStateId { get; set; }
    public Guid NewUserStateId { get; set; }
    public Guid NewMedicalRecordStateId { get; set; }

}
