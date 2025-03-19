using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Interfaces.MedicalRecords;
using Domain.Interfaces.Messages;
using Domain.Interfaces.Users;
using Notifications;
using Lib.MessageQueues.Functions.Repositories.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Queues.Functions.Models;
using Shared;
using StateMachines;

namespace Infraestructure.Repositories.MedicalRecords;

public class ProcessMedicalRecordRepository : IProcessMedicalRecordRepository
{
    #region Variables
    private readonly ApplicationDbContext _context;
    private readonly NotificationRepository _NotificationRepository;
    private readonly IUserRepository _IUserRepository;
    private readonly IOptions<RabbitMQSettingDTO> _rabbitMQSettings;
    private readonly GetMachineStateValidator _GetMachineStateValidator;
    private readonly IMessageService _IMessageService;
    private readonly EntityService _entityService;
    #endregion

    #region Ctor
    public ProcessMedicalRecordRepository(ApplicationDbContext context, NotificationRepository NotificationRepository, GetMachineStateValidator GetMachineStateValidator, IUserRepository IUserRepository, IOptions<RabbitMQSettingDTO> rabbitMQSettings, IMessageService IMessageService, EntityService entityService)
    {
        _context = context;
        _NotificationRepository = NotificationRepository;
        _IUserRepository = IUserRepository;
        _rabbitMQSettings = rabbitMQSettings;
        _GetMachineStateValidator = GetMachineStateValidator;
        _IMessageService = IMessageService;
        _entityService = entityService;
    }
    #endregion

    #region Public Methods
    /* Función que dispara mensaje en cola Pendiente según el proceso seleccionado */
    public async Task<RequestResult> CreateAttention(string ProcessCode, Guid PatientId)
    {
        (bool sucessMachineState, string messageMachineState, EntityEventStateMachine entityMachineState) = await GetMachineStates(PatientId, null, null, StateEventProcessEnum.CREATED);
        if (!sucessMachineState) return RequestResult.ErrorResult(messageMachineState);
        dynamic? Patient = await GetInfoPatient(PatientId);
        if (Patient == null) return RequestResult.ErrorResult(message: _IMessageService.GetThePatientDoesNotExist());
        Service? Service = await GetService(ProcessCode);
        if (Service == null) return RequestResult.ErrorResult(message: _IMessageService.GetTheCodeServiceDoesNotExist());
        (bool SucessGetQueueNameConfig, string ResultGetQueueName, Guid QueueConfId) = await GetQueueNameConfig(ProcessCode, new { Patient.LevelQueueCode, Patient.CountryId, Patient.DepartmentId, Patient.CityId });
        if (!SucessGetQueueNameConfig) return RequestResult.ErrorResult(message: ResultGetQueueName);
        int Priority = GetPriority((DateTime)Patient.Birthday, Patient.Comorbidities, Patient.PlanCodeNumber);
        var (sucessProcessEmitAttention, resultProcessEmitAttention, NewAttention) = await TriggerEmitAttention(entityMachineState.NewMedicalRecordStateId, PatientId, entityMachineState.NewPatientStateId, QueueConfId, ResultGetQueueName, Priority, Service.Id);
        if (!sucessProcessEmitAttention) return RequestResult.ErrorRecord(message: resultProcessEmitAttention);
        var GetUserAvailable = await _IUserRepository.SearchFirstUserAvailable(ProcessCode);
        if (GetUserAvailable?.Data != null)
            return await AssignAttention((Guid)GetUserAvailable.Data);
        var GetAttention = await GetAttentionByIdAsNoTracking(NewAttention);
        return RequestResult.SuccessRecord(message: _IMessageService.GetSuccessCreation(), data: GetAttention);
    }
    /* Función que dispara mensaje en cola Asignado según el proceso seleccionado */
    public async Task<RequestResult> AssignAttention(Guid UserId)
    {
        (bool sucessMachineState, string messageMachineState, EntityEventStateMachine entityEventStateMachine) = await GetMachineStates(null, UserId, null, StateEventProcessEnum.ASSIGNED);
        if (!sucessMachineState) return RequestResult.ErrorResult(messageMachineState);
        User? User = await GetUserById(UserId);
        if (User == null)
            return RequestResult.ErrorResult(_IMessageService.GetTheUserDoesNotExist());
        string? ProcessCode = User.UserServices.Where(x => x.ServicePriority).FirstOrDefault()?.Service.Code;
        if (string.IsNullOrEmpty(ProcessCode)) return RequestResult.ErrorResult(_IMessageService.GetTheUserDoesNotRelatedPriorityService());
        (bool SucessGetQueueNameConfig, string ResultGetQueueName, Guid QueueConfId) = await GetQueueNameConfig(ProcessCode, new { LevelQueueCode = User.BusinessLine.LevelQueue.Code, User.City.Department.CountryId, User.City.DepartmentId, User.CityId });
        if (!SucessGetQueueNameConfig)
            return RequestResult.ErrorResult(ResultGetQueueName);
        var (SucessConsumeMessage, ResultConsumeMessage, AttentionId) = await ConsumeMessage(ResultGetQueueName, User.Id, entityEventStateMachine);
        if (!SucessConsumeMessage)
            return RequestResult.ErrorResult(ResultConsumeMessage);
        var Attention = await GetAttentionByIdAsNoTracking(AttentionId);
        return RequestResult.SuccessRecord(message: _IMessageService.GetSuccessAssignation(), data: Attention);
    }
    /* Función que dispara mensaje en cola En Proceso según el proceso seleccionado */
    public async Task<RequestResult> StartAttention(Guid AttentionId) => await ProcessAttention(AttentionId, StateEventProcessEnum.INPROCESS);
    /* Función que dispara mensaje en cola Finalizado según el proceso seleccionado */
    public async Task<RequestResult> FinishAttention(Guid AttentionId) => await ProcessAttention(AttentionId, StateEventProcessEnum.FINALIZED);
    /* Función que cancela la atención */
    public async Task<RequestResult> CancelAttention(Guid AttentionId) => await ProcessAttention(AttentionId, StateEventProcessEnum.CANCELLED);
    /* Función que consulta parametrización a nivel de procesos, ciudad, departamento, pais y linea de negocio*/
    public async Task<(bool, string, Guid)> GetQueueNameConfig(string? ProcessCode, dynamic? ObjUser)
    {
        string Result = string.Empty;
        Guid? QueueConfId = Guid.Empty;
        if (Enum.TryParse(ObjUser?.LevelQueueCode, out LevelEnum levelProcess))
        {
            string LevelQueueCode = ObjUser?.LevelQueueCode ?? "";

            IQueryable<GeneratedQueue> query = _context.GeneratedQueues
                .Include(x => x.QueueConf).ThenInclude(x => x.BusinessLineLevelValueQueueConf)
                .Where(x => x.QueueConf.BusinessLineLevelValueQueueConf.Service.Code.Equals(ProcessCode))
                .Where(x => x.QueueConf.BusinessLineLevelValueQueueConf.LevelQueue.Code.Equals(LevelQueueCode));

            switch (levelProcess)
            {
                case LevelEnum.COUNTRY:
                    Guid countryPatient = ObjUser?.CountryId;
                    query = query.Where(x => x.QueueConf.BusinessLineLevelValueQueueConf.CountryId.Equals(countryPatient));
                    break;
                case LevelEnum.DEPARTMENT:
                    Guid departmentPatient = ObjUser?.DepartmentId;
                    query = query.Where(x => x.QueueConf.BusinessLineLevelValueQueueConf.DepartmentId.Equals(departmentPatient));
                    break;
                case LevelEnum.CITY:
                    Guid cityPatient = ObjUser?.CityId;
                    query = query.Where(x => x.QueueConf.BusinessLineLevelValueQueueConf.CityId.Equals(cityPatient));
                    break;
            }
            var result = await query.FirstOrDefaultAsync();
            if (result == null)
                return (false, _IMessageService.GetDoesNotExistQueueCombinations(), Guid.Empty);
            Result = result.Name;
            QueueConfId = result.QueueConfId;
        }
        return (true, Result, (Guid)QueueConfId);
    }
    #endregion

    #region Private Methods
    /* Función que procesa una atención con estado Pendiente y publica mensaje a Encolador de mensajerias */
    private async Task<(bool, string, Guid?)> TriggerEmitAttention(Guid NewMedicalRecordStateId, Guid PatientId, Guid? PatientStateId, Guid QueueConfId, string QueueName, int Priority, Guid ProcessId)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                Patient? Patient = await GetPatientById(PatientId);
                if (Patient == null) return (false, _IMessageService.GetThePatientDoesNotExist(), null);
                Guid newMedicalRecord = Guid.NewGuid();
                var MedicalRecordTask = AddAttention(newMedicalRecord, ProcessId, PatientId, NewMedicalRecordStateId, Priority);
                var HistoryTask = AddAttentionHistory(newMedicalRecord, NewMedicalRecordStateId);
                var UpdatePatientTask = UpdatePatientState(Patient, PatientStateId);
                var ProcessMessageTask = AddProcessMessage(newMedicalRecord, QueueConfId);
                await Task.WhenAll(MedicalRecordTask, HistoryTask, UpdatePatientTask, ProcessMessageTask);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                await PublishMessageWithErrorHandling(QueueName, newMedicalRecord.ToString(), (byte)Priority, await ProcessMessageTask);
                return (true, _IMessageService.GetSuccessCreation(), newMedicalRecord);
            }
            catch (Exception ex)
            {
                // Si algo falla, revertimos los cambios
                await transaction.RollbackAsync();
                return (false, $"Error procesando la atención: {ex.Message}", null);
            }
        }
    }
    /* Función que procesa una atención con estado Asignado y consume mensaje a Encolador de mensajerias */
    public async Task<(bool, string)> TriggerAsignAttention(Guid AttentionId, Guid AttentionStateId, Guid PatientStateId, Guid UserId, Guid HealCareStaffStateId)
    {
        // Empezamos la transacción explícita
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var Attention = await GetAttentionById(AttentionId);
                if (Attention == null) return (false, _IMessageService.GetTheMedicalRecordDoesNotExist());
                var Patient = await GetPatientById(Attention.PatientId);
                if (Patient == null) return (false, _IMessageService.GetThePatientDoesNotExist());
                var User = await GetUserById(UserId);
                if (User == null) return (false, _IMessageService.GetTheUserDoesNotExist());
                var getProcessMessage = await GetProcessMessage(AttentionId);
                if (getProcessMessage == null) return (false, _IMessageService.GetDontPossibleProcessCreation());

                // Procesos en paralelo que siempre se ejecutan
                var historyTask = AddAttentionHistory(Attention.Id, AttentionStateId);
                var updateStaffStateTask = UpdateUserState(User, HealCareStaffStateId);
                var updatePatientTask = UpdatePatientState(Patient, PatientStateId);
                var updateProcessMessgeTask = UpdateProcessMessage(getProcessMessage);
                var updateAttentionStateTask = UpdateAttentionState(Attention, AttentionStateId, false, User.Id);
                await Task.WhenAll(new List<Task> { historyTask, updateStaffStateTask, updatePatientTask, updateProcessMessgeTask, updateAttentionStateTask });
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return (true, _IMessageService.GetSuccessAssignation());
            }
            catch (Exception ex)
            {
                // Si hay algún error, revertimos la transacción
                await transaction.RollbackAsync();
                return (false, "Hubo un problema al realiza la trasacción de asignación de la cita: " + ex.Message);
            }
        }
    }
    /* Función que procesa la transacción en Estado (En proceso - Finalizado - Cancelado) */
    public async Task<(bool, string)> TriggerProcessAttention(Guid PatientId, Guid PatientStateId, Guid AttentionStateId, Guid HealCareStaffId, Guid HealCareStaffStateId, Guid AttentionId, bool ApplyClosed = false)
    {
        // Empezamos la transacción explícita
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {

                var patient = await GetPatientById(PatientId);
                var user = await GetUserById(HealCareStaffId);
                var attention = await GetAttentionById(AttentionId);

                if (attention == null) throw new Exception("Doesnt exist Attention");
                if (patient == null) throw new Exception("Doesnt exist Patient");
                if (user == null) throw new Exception("Doesnt exist User");

                // Procesos en paralelo, ya que el DbContext se maneja correctamente
                var historyTask = AddAttentionHistory(attention.Id, AttentionStateId);
                var updatePatientTask = UpdatePatientState(patient, PatientStateId);
                var updateStaffStateTask = UpdateUserState(user, HealCareStaffStateId);
                var UpdateAttentionTask = UpdateAttentionState(attention, AttentionStateId, ApplyClosed, null);

                // Esperamos que todas las tareas terminen
                await Task.WhenAll(historyTask, updatePatientTask, updateStaffStateTask, UpdateAttentionTask);

                // Guardamos todos los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Confirmamos la transacción
                await transaction.CommitAsync();

                return (true, _IMessageService.GetSucessOperation());
            }
            catch (Exception ex)
            {
                // Si hay algún error, revertimos la transacción
                await transaction.RollbackAsync();
                return (false, $"{_IMessageService.GetErrorOperation()} : {ex.Message}");
            }
        }
    }
    /* Método que publica mensaje en rabbit y maneja errores */
    private async Task PublishMessageWithErrorHandling(string queueName, string message, byte priority, ProcessMessage processMessage)
    {
        try
        {
            using (var publisher = new RabbitMQPublisher(_rabbitMQSettings))
            {
                var (pubSuccess, pubMessage) = await publisher.PublishMessageAsync(queueName, message, priority);
                if (pubSuccess)
                {
                    processMessage.Published = true;
                    processMessage.PublishedAt = DateTime.Now;
                    _context.ProcessMessages.Update(processMessage);
                    await _context.SaveChangesAsync();
                }
                else
                    await LogErrorAsync("Error Publish", null, pubMessage);
            }
        }
        catch (Exception ex)
        {
            await LogErrorAsync("Exception Publish", ex);
        }
    }
    /* Función que permita armar el objeto AttentionHistory */
    private async Task AddAttentionHistory(Guid attentionId, Guid? AttentionStateId) => await _context.MedicalRecordHistories.AddAsync(new MedicalRecordHistory { Id = Guid.NewGuid(), MedicalRecordId = attentionId, MedicalRecordStateId = AttentionStateId?? Guid.Empty, CreatedAt = DateTime.Now });
    /* Función que guarda mensaje de error */
    private async Task LogErrorAsync(string reason, Exception? ex = null, string? message = null)
    {
        var logError = new ProcessMessageErrorLog
        {
            Id = Guid.NewGuid(),
            ErrorMessage = ex != null && ex.Message != null ? ex.Message : !string.IsNullOrEmpty(message) ? message : "No error messages",
            StackTrace = ex?.StackTrace ?? "No stack trace",
            Reason = reason,
            CreatedAt = DateTime.Now
        };

        await _context.ProcessMessageErrorLogs.AddAsync(logError);
        await _context.SaveChangesAsync();
    }
    /* Función que registra el process message */
    private async Task<ProcessMessage> AddProcessMessage(Guid attentionId, Guid QueueConfId)
    {
        var processMessage = new ProcessMessage
        {
            Id = Guid.NewGuid(),
            MedicalRecordId = attentionId,
            QueueConfId = QueueConfId,
            Message = JsonConvert.SerializeObject(attentionId),
            CreatedAt = DateTime.Now,
            Published = false,
            PublishedAt = null
        };
        await _context.ProcessMessages.AddAsync(processMessage);
        return processMessage;
    }
    /* Función que obtiene la información process message */
    private async Task<ProcessMessage?> GetProcessMessage(Guid attentionId) => await _context.ProcessMessages.SingleOrDefaultAsync(x => x.MedicalRecordId.Equals(attentionId));
    /* Función que actualiza la información process message */
    private Task UpdateProcessMessage(ProcessMessage processMessage)
    {
        processMessage.Consumed = true;
        processMessage.ConsumedAt = DateTime.Now;
        _context.Attach(processMessage);
        _context.Entry(processMessage).Property(p => p.Consumed).IsModified = true;
        _context.Entry(processMessage).Property(p => p.ConsumedAt).IsModified = true;
        return Task.CompletedTask;
    }
    /* Función que actualiza el estado del paciente */
    private Task UpdatePatientState(Patient patient, Guid? patientStateId)
    {
        patient.PatientStateId = patientStateId ?? Guid.Empty;
        _context.Attach(patient);
        _context.Entry(patient).Property(p => p.PatientStateId).IsModified = true;
        return Task.CompletedTask;
    }
    /* Función que consulta la información del paciente */
    private async Task<Patient?> GetPatientById(Guid patientId) => await _context.Patients.FindAsync(patientId);
    /* Función que actualiza el estado del paciente */
    private Task UpdateUserState(User User, Guid PersonStateId)
    {
        User.UserStateId = PersonStateId;
        _context.Attach(User);
        _context.Entry(User).Property(p => p.UserStateId).IsModified = true;
        return Task.CompletedTask;

    }
    /* Función que actualiza el estado de la atención */
    private Task UpdateAttentionState(MedicalRecord attention, Guid stateAttentionId, bool applyClosed, Guid? userId)
    {
        attention.MedicalRecordStateId = stateAttentionId;
        attention.UserId = userId ?? attention.UserId;
        attention.Open = applyClosed ? false : attention.Open;
        _context.Attach(attention);
        _context.Entry(attention).Property(p => p.MedicalRecordStateId).IsModified = true;
        _context.Entry(attention).Property(p => p.UserId).IsModified = true;
        _context.Entry(attention).Property(p => p.Open).IsModified = true;
        return Task.CompletedTask;
    }
    /* Función que guarda la atención  */
    private async Task AddAttention(Guid MedicalRecordId, Guid ProcessId, Guid? PatientId, Guid AttentionStateId, int? Priority)
    {
        var attention = new MedicalRecord { Id = MedicalRecordId, ServiceId = ProcessId, PatientId = PatientId ?? Guid.Empty, Open = true, CreatedAt = DateTime.Now, StartedAt = DateTime.Now, Comments = $"Cita creada a las {DateTime.Now}", Active = true, Priority = Priority, MedicalRecordStateId = AttentionStateId };
        await _context.MedicalRecords.AddAsync(attention);
    }
    /* Función que consulta el proceso por código */
    private async Task<Service?> GetService(string code) => await _context.Services.AsNoTracking().Where(x => x.Code == code).SingleOrDefaultAsync();
    /* Función que consulta la información del paciente */
    private async Task<dynamic?> GetInfoPatient(Guid patientId) => await _context.Patients.AsNoTracking().Include(x => x.City).Include(x => x.Plan).Select(x => new { x.Id, x.City.DepartmentId, x.City, x.CityId, x.City.Department.CountryId, PlanCode = x.Plan != null ? x.Plan.Code : string.Empty, PlanCodeNumber = x.Plan != null ? x.Plan.Number : 0, x.Birthday, x.Comorbidities, LevelQueueCode = x.BusinessLine.LevelQueue.Code, }).SingleOrDefaultAsync(x => x.Id == patientId);
    /* Función que consulta el personal asistencial por código */
    private async Task<User?> GetUserById(Guid? UserId)
    => await _context.Users.Where(x => x.Id == UserId).Include(x => x.UserServices).ThenInclude(x => x.Service).Include(x => x.BusinessLine).ThenInclude(x => x.LevelQueue).Include(x => x.City).ThenInclude(x => x.Department).FirstOrDefaultAsync();
    /* Función que consulta la atención por Id */
    private async Task<MedicalRecord?> GetAttentionById(Guid AttentionId) => await _context.MedicalRecords.FindAsync(AttentionId);
    /* Función que consulta la atención por Id */
    private async Task<dynamic?> GetAttentionByIdAsNoTracking(Guid? AttentionId) => await _context.MedicalRecords.AsNoTracking().Include(x => x.User).Where(x => x.Id.Equals(AttentionId))
        .Select(x =>
        new
        {
            AttentionId = x.Id,
            x.Priority,
            User = x.User != null ? x.User.Name : "N/A",
            Patient = x.Patient != null ? x.Patient.FirstName : "N/A",
            Process = x.Service != null ? x.Service.Name : string.Empty,
            City = x.Patient != null && x.Patient.City != null ? x.Patient.City.Name : string.Empty,
            PatientNum = x.Patient != null ? x.Patient.Identification : string.Empty,
            Comorbidities = x.Patient != null ? x.Patient.Comorbidities : 0,
            Age = x.Patient != null && x.Patient.Birthday != null ? CalculatedAge.YearsMonthsDays(Convert.ToDateTime(x.Patient.Birthday)) : string.Empty,
            State = x.MedicalRecordState != null ? x.MedicalRecordState.Name : string.Empty,
            Plan = x.Patient != null && x.Patient.Plan != null ? x.Patient.Plan.Name : "N/A",
            StartDate = x.CreatedAt.HasValue ? x.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty,
            EndDate = x.EndDate.HasValue ? x.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty,
            PatientId = x.Patient != null ? x.Patient.Id : Guid.Empty,
            UserId = x.User != null ? x.User.Id : Guid.Empty,
            CityId = x.User != null ? x.User.CityId : Guid.Empty,
            DepartmentId = x.User != null && x.User.City != null ? x.User.City.DepartmentId : Guid.Empty,
            CountryId = x.User != null && x.User.City != null ? x.User.City.Department.CountryId : Guid.Empty,
            ProcessId = x.User != null && x.User.UserServices.Any(s => s.ServicePriority) ? 
                x.User.UserServices.First(x=>x.ServicePriority == true).ServiceId: Guid.Empty,
            x.MedicalRecordStateId,
            processCode = x.User != null && x.User.UserServices.Any(x => x.ServicePriority) ?
                  x.User.UserServices.First(x => x.ServicePriority == true).Service.Code : string.Empty, 

        }).SingleOrDefaultAsync();
    /* Función que realiza proceso de proceso genericos */
    private async Task<RequestResult> ProcessAttention(Guid AttentionId, StateEventProcessEnum EventProcess)
    {
        MedicalRecord? MedicalRecord = await GetAttentionById(AttentionId);
        if (MedicalRecord == null) return RequestResult.ErrorResult(_IMessageService.GetTheMedicalRecordDoesNotExist());

        (bool sucessMachineState, string messageMachineState, EntityEventStateMachine entityMachineState) = await GetMachineStates(MedicalRecord.PatientId, MedicalRecord.UserId, MedicalRecord.Id, EventProcess);
        if (!sucessMachineState) return RequestResult.ErrorResult(messageMachineState);


        User? User = await GetUserById(MedicalRecord.UserId);
        if (User == null) return RequestResult.ErrorResult(_IMessageService.GetTheUserDoesNotExist());

        var (SucessTriggerProcessAttention, ResultTriggerProcessAttention) = await TriggerProcessAttention(MedicalRecord.PatientId, entityMachineState.NewPatientStateId, entityMachineState.NewMedicalRecordStateId, User.Id, (Guid)entityMachineState.NewUserStateId, AttentionId, EventProcess == StateEventProcessEnum.FINALIZED || EventProcess == StateEventProcessEnum.CANCELLED ? true : false);
        if (!SucessTriggerProcessAttention)
            return RequestResult.ErrorRecord(message: ResultTriggerProcessAttention);

        string? ProcessCode = User.UserServices.Where(x => x.ServicePriority).FirstOrDefault()?.Service.Code;
        if (string.IsNullOrEmpty(ProcessCode)) return RequestResult.ErrorResult(_IMessageService.GetTheUserDoesNotRelatedPriorityService());

        /* Si hay médico disponible, asignamos la cita automaticamente */
        if (EventProcess == StateEventProcessEnum.FINALIZED || EventProcess == StateEventProcessEnum.CANCELLED)
        {
            var getHealCareStaffAvailable = await _IUserRepository.SearchFirstUserAvailable(ProcessCode);
            if (getHealCareStaffAvailable?.Data != null)
            {
                var resultAssigned = await AssignAttention((Guid)getHealCareStaffAvailable.Data);
                if (resultAssigned.Success) return resultAssigned;
            }
        }
        var resultAttention = await GetAttentionByIdAsNoTracking(AttentionId);
        string processResult = GetProcessResult(EventProcess, AttentionId.ToString(), resultAttention);
        return RequestResult.SuccessRecord(data: resultAttention, message: processResult);
    }
    /* Función que calcula la prioridad del mensaje con base a la edad del paciente, comorbilidades y plan relacionado */
    private int? GetPriority(DateTime birthDate, int? comorbidities, int planRecord)
    {
        int age = DateTime.Now.Year - birthDate.Year;
        if (DateTime.Now < birthDate.AddYears(age))
            age--;
        int? priority = comorbidities;
        if (age >= 18 && age < 60)
            priority += 1;
        else
            priority += 2;
        priority += planRecord;
        if (priority == null) priority = 0;
        return priority;
    }
    /* Función que devuelve resultado string y emite evento de SignalR */
    private string GetProcessResult(StateEventProcessEnum StateEventProcessEnum, string AttentionId, dynamic resultAttention)
    {
        switch (StateEventProcessEnum)
        {
            case StateEventProcessEnum.INPROCESS:
                return _IMessageService.GetSuccessInProcess();

            case StateEventProcessEnum.FINALIZED:
                return _IMessageService.GetSucessFinish();

            case StateEventProcessEnum.CANCELLED:
                return _IMessageService.GetSucessCancel();

            default:
                return _IMessageService.GetSucessOperation();
        }
    }
    /* Función que permite consumir un mensaje en el orquestador de mensajeria */
    public async Task<(bool, string, Guid)> ConsumeMessage(string queueName, Guid UserId, EntityEventStateMachine MachineStates)
    {
        using (var consumer = new RabbitMQConsumer(_rabbitMQSettings))
        {
            var (success, message, deliveryTag) = await consumer.ConsumeMessageAsync(queueName);
            if (success)
            {
                var (sucessProcessAsignAttention, resultProcessAsignAttention) = await TriggerAsignAttention(Guid.Parse(message), (Guid)MachineStates.NewMedicalRecordStateId, MachineStates.NewPatientStateId, UserId, (Guid)MachineStates.NewUserStateId);
                if (!sucessProcessAsignAttention)
                {
                    var resultNacknowledgeMessageAsync = await consumer.NacknowledgeMessageAsync(deliveryTag);
                    return (resultNacknowledgeMessageAsync.success, resultNacknowledgeMessageAsync.message, Guid.Parse(message));
                }
                else
                {
                    var resultAcknowledgeMessageAsync = await consumer.AcknowledgeMessageAsync(deliveryTag);
                    return (resultAcknowledgeMessageAsync.success, resultAcknowledgeMessageAsync.message, Guid.Parse(message));
                }
            }
            else
                return (success, message, Guid.Empty);
        }
    }

    /* Función que consulta el primer mensajes de una cola */
    private async Task<(bool, string, int)> GetFirstMessage(string queueName)
    {
        var result = await (from gq in _context.GeneratedQueues
                            join pm in _context.ProcessMessages on gq.QueueConfId equals pm.QueueConfId
                            join att in _context.MedicalRecords on pm.MedicalRecordId equals att.Id
                            where gq.Name == queueName && att.MedicalRecordState.Code == MedicalRecordStateEnum.PENDING.ToString()
                            orderby pm.CreatedAt.Date descending,
                                    pm.CreatedAt.Hour descending,
                                    pm.CreatedAt.Minute descending,
                                    att.Priority descending
                            select new { pm.MedicalRecordId, pm.CreatedAt, att.Priority })
                            .FirstOrDefaultAsync();

        if (result != null)
            return (true, result.MedicalRecordId.ToString(), 0);
        return (false, "", 0);
    }
    /* Función que consulta la información de maquina de estados */
    private async Task<(bool, string, EntityEventStateMachine)> GetMachineStates(Guid? PatientId, Guid? UserId, Guid? MedicalRecordId, StateEventProcessEnum stateEvent)
    {
        var entityEvent = new EntityEventStateMachine { EventType = stateEvent, UserId = UserId, PatientId = PatientId, MedicalRecordId = MedicalRecordId };
        (bool successValidateEntity, string messageValidateEntity) = await _entityService.ValidateEntityEvent(entityEvent);
        if (successValidateEntity)
        {
            (bool successEvent, string messageEvent) = await _entityService.HandleEntityEvent(entityEvent);
            return (successEvent, messageEvent, entityEvent);
        }
        else
            return (false, messageValidateEntity, entityEvent);
    }

    #endregion
}