using Application.Data;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Messages;
using Microsoft.EntityFrameworkCore;

namespace StateMachines
{
    public class GetMachineStateValidator
    {
        private readonly IApplicationDbContext _context;
        private readonly IMessageService _IMessageService;
        public GetMachineStateValidator(IApplicationDbContext context, IMessageService IMessageService)
        {
            _context = context;
            _IMessageService = IMessageService;
        }
        /* Precondiciones para la creación de una atención */
        public async Task<(bool, string)> CreationPreconditions(EntityEventStateMachine entityEvent)
        {
            var patient = await GetInfoPatient(entityEvent.PatientId);
            if (patient == null) 
                return (false, _IMessageService.GetThePatientDoesNotExist());
            var validationCanNotCreateAttention = await CanNotCreateAttention(entityEvent.PatientId);
            if (validationCanNotCreateAttention == true) return (false, _IMessageService.GetThePatientHasAnOngoingAttention());
            entityEvent.ActualPatientStateCode = (PatientStateEnum)Enum.Parse(typeof(PatientStateEnum), patient.PatientState.Code);
            return (true, _IMessageService.GetSucessOperation());
        }
        /* Precondiciones para la asignación de una atención */
        public async Task<(bool, string)> AsignationPreconditions(EntityEventStateMachine entityEvent)
        {
            var User = await GetUser(entityEvent.UserId);
            if (User == null) return (false, _IMessageService.GetTheUserDoesNotExist());
            entityEvent.ActualUserStateCode = (UserStateEnum)Enum.Parse(typeof(UserStateEnum), User.UserState.Code);
            return (true, _IMessageService.GetSucessOperation());
        }

        /* Precondiciones para el inicio de una atención */
        public async Task<(bool, string)> InProcessPreconditions(EntityEventStateMachine entityEvent)
        {
            var MedicalRecord = await GetInfoAttention(entityEvent.MedicalRecordId);
            if (MedicalRecord == null) return (false, _IMessageService.GetTheMedicalRecordDoesNotExist());

            var Patient = await GetInfoPatient(entityEvent.PatientId);
            if (Patient == null) return (false, _IMessageService.GetThePatientDoesNotExist());

            var User = await GetUser(entityEvent.UserId);
            if (User == null) return (false, _IMessageService.GetTheUserDoesNotExist());

            entityEvent.ActualPatientStateCode = (PatientStateEnum)Enum.Parse(typeof(PatientStateEnum), Patient.PatientState.Code);
            entityEvent.ActualUserStateCode = (UserStateEnum)Enum.Parse(typeof(UserStateEnum), User.UserState.Code);
            entityEvent.ActualMedicalRecordStateCode = (MedicalRecordStateEnum)Enum.Parse(typeof(MedicalRecordStateEnum), MedicalRecord.MedicalRecordState.Code);

            return (true, _IMessageService.GetSucessOperation());
        }

        /* Precondiciones para la finalización de una atención */
        public async Task<(bool, string)> FinalizationPreconditions(EntityEventStateMachine entityEvent)
        {
            var Patient = await GetInfoPatient(entityEvent.PatientId);
            if (Patient == null) return (false, _IMessageService.GetThePatientDoesNotExist());

            var User = await GetUser(entityEvent.UserId);
            if (User == null) return (false, _IMessageService.GetTheUserDoesNotExist());

            var MedicalRecord = await GetInfoAttention(entityEvent.MedicalRecordId);
            if (MedicalRecord == null) return (false, _IMessageService.GetTheMedicalRecordDoesNotExist());

            entityEvent.ActualPatientStateCode = (PatientStateEnum)Enum.Parse(typeof(PatientStateEnum), Patient.PatientState.Code);
            entityEvent.ActualUserStateCode = (UserStateEnum)Enum.Parse(typeof(UserStateEnum), User.UserState.Code);
            entityEvent.ActualMedicalRecordStateCode = (MedicalRecordStateEnum)Enum.Parse(typeof(MedicalRecordStateEnum), MedicalRecord.MedicalRecordState.Code);


            return (true, _IMessageService.GetSucessOperation());
        }
        /* Precondiciones para la cancelación de una atención */
        public async Task<(bool, string)> CancellationPreconditions(EntityEventStateMachine entityEvent)
        {
            var Patient = await GetInfoPatient(entityEvent.PatientId);
            if (Patient == null) return (false, _IMessageService.GetThePatientDoesNotExist());

            if (entityEvent.UserId != null && entityEvent.UserId != Guid.Empty)
            {
                var User = await GetUser(entityEvent.UserId);
                if (User == null) return (false, _IMessageService.GetTheUserDoesNotExist());
                entityEvent.ActualUserStateCode = (UserStateEnum)Enum.Parse(typeof(UserStateEnum), User.UserState.Code);

            }
            var MedicalRecord = await GetInfoAttention(entityEvent.MedicalRecordId);
            if (MedicalRecord == null) return (false, _IMessageService.GetTheMedicalRecordDoesNotExist());

            entityEvent.ActualPatientStateCode = (PatientStateEnum)Enum.Parse(typeof(PatientStateEnum), Patient.PatientState.Code);
            entityEvent.ActualMedicalRecordStateCode = (MedicalRecordStateEnum)Enum.Parse(typeof(MedicalRecordStateEnum), MedicalRecord.MedicalRecordState.Code);
            return (true, _IMessageService.GetSucessOperation());
        }

        #region Private Methods
        /* Función que consulta la información de un paciente */
        private async Task<Patient?> GetInfoPatient(Guid? PatientId) => await _context.Patients.AsNoTracking().Include(x => x.PatientState).SingleOrDefaultAsync(x => x.Id.Equals(PatientId));
        /* Función que consulta la información de un personal asistencial */
        private async Task<User?> GetUser(Guid? HealthCareStaffId) => await _context.Users.AsNoTracking().Include(x => x.UserState).SingleOrDefaultAsync(x => x.Id.Equals(HealthCareStaffId));
        /* Función que consulta la información de una atención */
        private async Task<MedicalRecord?> GetInfoAttention(Guid? AttentionId) => await _context.MedicalRecords.AsNoTracking().Include(x => x.MedicalRecordState).SingleOrDefaultAsync(x => x.Id.Equals(AttentionId));
        /* Función que consulta si el paciente tiene una atención abierta en el sistema */
        private async Task<bool> CanNotCreateAttention(Guid? Patient) => await _context.MedicalRecords.Where(x => x.PatientId.Equals(Patient) && x.Open == true && x.Active == true).AnyAsync();
        #endregion
    }
}
