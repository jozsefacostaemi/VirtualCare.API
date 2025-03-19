using Auth.VirtualCare.API;
using Auth.VirtualCare.Domain.Interfaces.Auth;
using Auth.VirtualCare.Domain.Interfaces.AuthomatedAuth;
using Auth.VirtualCare.Domain.Interfaces.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.VirtualCare.Infraestructure.Repositories
{
    public class AuthomatedAuthRepository : IAuthomatedAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthRepository _authRepository;
        public AuthomatedAuthRepository(ApplicationDbContext context, IAuthRepository authRepository)
        {
            _context = context;
            _authRepository = authRepository;
        }

        #region Public Methods
        /* Función que permite el login del usuario */
        public async Task<RequestResult> AuthomatedLogin(int? number)
        {
            if (number == null || number < 0)
                number = 100000;

            var users = await _context.Users
                .Where(x => x.Loggued == false).Take((int)number)
                .Select(x => new { x.UserName, x.Password })
                .ToListAsync();

            List<RequestResult> lstrequest = new List<RequestResult>();
            foreach (var item in users)
                lstrequest.Add(await _authRepository.Login(item.UserName, item.Password));
            return RequestResult.SuccessOperation(data: lstrequest);

        }
        /* Función que cierra la sesión del personal asistencial */
        public async Task<RequestResult> AuthomatedLogOut(int? number)
        {
            if (number == null || number < 0)
                number = 100000;

            var users = await _context.Users
                .Where(x => x.Loggued == true).Take((int)number)
                .Select(x => new { x.Id })
                .ToListAsync();

            List<RequestResult> lstrequest = new List<RequestResult>();
            foreach (var item in users)
                lstrequest.Add(await _authRepository.LogOut(item.Id));
            return RequestResult.SuccessOperation(data: lstrequest);
        }

        #endregion
    }
}
