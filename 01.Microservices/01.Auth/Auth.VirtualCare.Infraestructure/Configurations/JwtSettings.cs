using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.VirtualCare.Infraestructure.Configurations;
public class JwtSettings
{
    public string? SecretKey { get; set; }
    public int HashSize { get; set; }
    public int Iterations { get; set; }
    public int ExpiresMinutes { get; set; }
    public int SaltSize { get; set; }
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
}
