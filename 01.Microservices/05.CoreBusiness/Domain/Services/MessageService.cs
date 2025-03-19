using Domain.Interfaces.Messages;
using Domain.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Services
{
    public class MessageService : IMessageService
    {

        private readonly IStringLocalizer<Messages> _localizer;
        public MessageService(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;
        }
        public string GetTransitionValidMessage() => _localizer[Messages.TransitionValid];
        public string GetInvalidPatientStateForCreationMessage() => _localizer[Messages.InvalidStateForCreation];
        public string GetInvalidStateForAssignmentMessage() => _localizer[Messages.InvalidStateForAsignation];
        public string GetInvalidStateForProcessStartMessage() => _localizer[Messages.InvalidStateForInProcess];
        public string GetInvalidStateForFinalizationMessage() => _localizer[Messages.InvalidStateForFinish];
        public string GetInvalidStateForCancellationMessage() => _localizer[Messages.InvalidStateForCancel];
        public string GetValidEventTypeMessage() => _localizer[Messages.TransitionValid];
        public string GetInvalidEventTypeMessage() => _localizer[Messages.TransitionInvalid];
        public string GetSucessOperation() => _localizer[Messages.SuccessOperation];
        public string GetTheMedicalRecordDoesNotExist() => _localizer[Messages.TheMedicalRecordDoesNotExist];
        public string GetThePatientDoesNotExist() => _localizer[Messages.ThePatientDoesNotExist];
        public string GetThePatientHasAnOngoingAttention() => _localizer[Messages.ThePatientHasAnOngoingAttention];
        public string GetTheUserDoesNotExist() => _localizer[Messages.TheUserDoesNotExist];
        public string GetInformationNotFound() => _localizer[Messages.InformationNotFound];
        public string GetDontPossibleProcessCreation() => _localizer[Messages.DontPossibleProcessCreation];
        public string GetErrorOperation() => _localizer[Messages.ErrorOperation];
        public string GetSuccessAssignation() => _localizer[Messages.SuccessAssignation];
        public string GetSuccessCreation() => _localizer[Messages.SuccessCreation];
        public string GetSuccessInProcess() => _localizer[Messages.SuccessInProcess];
        public string GetSucessCancel() => _localizer[Messages.SucessCancel];
        public string GetSucessFinish() => _localizer[Messages.SucessFinish];
        public string GetTheCodeServiceDoesNotExist() => _localizer[Messages.TheCodeServiceDoesNotExist];
        public string GetDoesNotExistQueueCombinations() => _localizer[Messages.DoesNotExistQueueCombinations];
        public string GetTheUserDoesNotRelatedPriorityService() => _localizer[Messages.TheUserDoesNotRelatedPriorityService];
        public string GetInvalidCredentials() => _localizer[Messages.InvalidCredentials];
        public string GetSuccessLogin() => _localizer[Messages.SuccessLogin];
    }
}
