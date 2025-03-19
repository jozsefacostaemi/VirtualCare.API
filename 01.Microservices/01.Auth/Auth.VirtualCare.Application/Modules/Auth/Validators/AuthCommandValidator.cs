using Auth.VirtualCare.Application.Modules.Auth.Commands;
using FluentValidation;

namespace Application.Modules.Auth.Validators
{
    public class AuthCommandValidator : AbstractValidator<LoginCommand>
    {
        public AuthCommandValidator()
        {
            RuleFor(r => r.userName).NotEmpty().WithName("Usuario");
            RuleFor(r => r.password).NotEmpty().WithName("Contraseña");
        }
    }
}

