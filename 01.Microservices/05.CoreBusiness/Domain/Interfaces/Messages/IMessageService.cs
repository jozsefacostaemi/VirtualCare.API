namespace Domain.Interfaces.Messages
{
    public interface IMessageService
    {
        public string GetTransitionValidMessage();
        public string GetInvalidPatientStateForCreationMessage();
        public string GetInvalidStateForAssignmentMessage();
        public string GetInvalidStateForProcessStartMessage();
        public string GetInvalidStateForFinalizationMessage();
        public string GetInvalidStateForCancellationMessage();
        public string GetInvalidEventTypeMessage();
        public string GetSucessOperation();
        public string GetTheMedicalRecordDoesNotExist();
        public string GetThePatientDoesNotExist();
        public string GetThePatientHasAnOngoingAttention();
        public string GetTheUserDoesNotExist();
        public string GetInformationNotFound();
        public string GetDontPossibleProcessCreation();
        public string GetErrorOperation();
        public string GetSuccessAssignation();
        public string GetSuccessCreation();
        public string GetSuccessInProcess();
        public string GetSucessCancel();
        public string GetSucessFinish();
        public string GetTheCodeServiceDoesNotExist();
        public string GetDoesNotExistQueueCombinations();
        public string GetTheUserDoesNotRelatedPriorityService();
        public string GetInvalidCredentials();
        public string GetSuccessLogin();
    }
}
