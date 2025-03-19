namespace Auth.VirtualCare.Domain.Interfaces.Messages;
public interface IMessageService
{
    public string GetInvalidCredentials();
    public string GetSuccessLogin();
}
