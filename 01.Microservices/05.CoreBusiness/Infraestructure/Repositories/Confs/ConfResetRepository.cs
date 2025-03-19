using Domain.Enums;
using Domain.Interfaces.Confs;
using Domain.Interfaces.Queues;
using Microsoft.EntityFrameworkCore;
using Shared;
using Web.Core.Business.API.Infraestructure.Persistence.Repositories.Queue;

namespace Infraestructure.Repositories.Confs
{
    public class ConfResetRepository : IConfResetRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IQueueRepository _IQueueRepository;
        public ConfResetRepository(ApplicationDbContext context, IQueueRepository IQueueRepository)
        {
            _context = context;
            _IQueueRepository = IQueueRepository;
        }

        #region Public Methods
        /* Función que devuelve la información de las atenciones */
        public async Task<RequestResult> ResetAttentionsAndPersonStatus()
        {
            await deleteProcessMessageErrorLog();
            await deleteProcessMessage();
            await deleteAttentionHistory();
            await deleteAttentions();
            await updateStatusPersons();
            await _IQueueRepository.GeneratedConfigQueues();
            return RequestResult.SuccessOperation();
        }
        #endregion

        #region Private Methods
        /* Función que elimina el historial de atenciones */
        private async Task deleteAttentionHistory()
        {
            var attentionHistories = await _context.MedicalRecordHistories.ToListAsync();
            _context.MedicalRecordHistories.RemoveRange(attentionHistories);
            await _context.SaveChangesAsync();
        }
        /* Función que elimina el proceso de los mensajes */
        private async Task deleteProcessMessage()
        {
            var processMessages = await _context.ProcessMessages.ToListAsync();
            _context.ProcessMessages.RemoveRange(processMessages);
            await _context.SaveChangesAsync();
        }

        /* Función que elimina el proceso de los mensajes */
        private async Task deleteProcessMessageErrorLog()
        {
            var processMessagesErrorLogs = await _context.ProcessMessageErrorLogs.ToListAsync();
            _context.ProcessMessageErrorLogs.RemoveRange(processMessagesErrorLogs);
            await _context.SaveChangesAsync();
        }

        /* Función que elimina las atenciones */
        private async Task deleteAttentions()
        {
            var attentions = await _context.MedicalRecords.ToListAsync();
            _context.MedicalRecords.RemoveRange(attentions);
            await _context.SaveChangesAsync();
        }
        /* Función que actualiza el estado de medicos y pacientes */
        private async Task updateStatusPersons()
        {
            Guid? getUserStatusAvailable = await _context.UserStates.Where(x => x.Code.Equals(UserStateEnum.AVAILABLE.ToString())).Select(x => x.Id).FirstOrDefaultAsync();
            Guid? getPatientStatusAvailable = await _context.PatientStates.Where(x => x.Code.Equals(PatientStateEnum.EMPTY.ToString())).Select(x => x.Id).FirstOrDefaultAsync();

            if (getUserStatusAvailable != null && getUserStatusAvailable != Guid.Empty)
            {
                await _context.Users.ExecuteUpdateAsync(x => x.SetProperty(p => p.UserStateId, getUserStatusAvailable).SetProperty(x => x.Loggued, false).SetProperty(p => p.AvailableAt, null as DateTime?).SetProperty(p=>p.Token,null as string));
                await _context.Patients.ExecuteUpdateAsync(x => x.SetProperty(p => p.PatientStateId, getPatientStatusAvailable));
            }
        }
        #endregion
    }
}
