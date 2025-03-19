using Auth.VirtualCare.API;
using Auth.VirtualCare.Domain.Interfaces.Auth;
using Auth.VirtualCare.Domain.Interfaces.Messages;
using Auth.VirtualCare.Infraestructure.Configurations;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.VirtualCare.Infraestructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly IMessageService _IMessageService;
        private readonly List<string> LstStatesCanNotLoggued = new List<string> { UserStateEnum.ASSIGNED.ToString(), UserStateEnum.ASSIGNED.ToString() };

        public AuthRepository(ApplicationDbContext context, IOptions<JwtSettings> jwtSettings, IMessageService IMessageService)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            _IMessageService = IMessageService;
        }

        #region Public Methods
        /* Función que permite el login del usuario */
        public async Task<RequestResult> Login(string username, string? password)
        {
            var user = await GetByUserName(username);
            if (user == null)
                return RequestResult.SuccessResultNoRecords(message: _IMessageService.GetInvalidCredentials());
            var isCorrectPassword = password?.Equals(user.Password);
            if (isCorrectPassword == false)
                return RequestResult.SuccessResultNoRecords(message: _IMessageService.GetInvalidCredentials());
            DateTime TokenExpiryDate = DateTime.Now.AddMinutes(_jwtSettings.ExpiresMinutes);
            var newToken = CreateJwt(user, TokenExpiryDate);
            user.TokenExpiryDate = TokenExpiryDate;
            user.Token = newToken;
            user.Loggued = true;
            user.AvailableAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return RequestResult.SuccessResult(message: _IMessageService.GetSuccessLogin(), data: newToken);
        }
        /* Función que cierra la sesión del personal asistencial */
        public async Task<RequestResult> LogOut(Guid UserId)
        {
            var user = await _context.Users.Include(x => x.UserState).Where(x => x.Id.Equals(UserId)).FirstOrDefaultAsync();
            if (user == null)
                return RequestResult.SuccessResultNoRecords(message: "El personal asistencial no existe");
            if (LstStatesCanNotLoggued.Contains(user.UserState.Code))
                return RequestResult.SuccessResultNoRecords(message: "El personal asistencial tiene una atención Asignada o En proceso");
            user.Loggued = false;
            user.AvailableAt = null;
            user.Token = null;
            user.TokenExpiryDate = null;
            await _context.SaveChangesAsync();
            return RequestResult.SuccessResult(message: "LogOut Exitoso", data: UserId);
        }
        #endregion

        #region Private Methods
        /* Función que valida si un usuario existe en la base de datos */
        private async Task<User?> GetByUserName(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;

            var result = await _context.Users
                 .Where(x => x.UserName.Equals(username))
                 .FirstOrDefaultAsync();

            return result;
        }
        /* Función que crea un JWT cuando la contraseña es correcta */
        private string CreateJwt(User user, DateTime DateExpires)
        {
            // Definir los Claims (información que contiene el token)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,$"{user.UserName}"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience != null ? _jwtSettings.Audience: string.Empty),
            };

            string jwt = _jwtSettings.SecretKey ?? throw new Exception("SecretKey Empty");

            // Crear la clave de seguridad (secreto compartido) que se usará para firmar el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el token JWT
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiresMinutes),
                signingCredentials: creds
            );

            // Generar el JWT en forma de cadena
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
