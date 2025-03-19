using Auth.VirtualCare.Domain.Interfaces.Messages;
using Auth.VirtualCare.Domain.Resources;
using Microsoft.Extensions.Localization;

namespace Auth.VirtualCare.Domain.Services
{
    public class MessageService : IMessageService
    {
        private readonly IStringLocalizer<Messages> _localizer;
        public MessageService(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;
        }
        public string GetInvalidCredentials() => _localizer[Messages.InvalidCredentials];
        public string GetSuccessLogin() => _localizer[Messages.SuccessLogin];
    }
}
