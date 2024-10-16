using FiotecInfodengue.Domain.Entities;
using FiotecInfodengue.Domain.Interfaces.Security;
using FiotecInfodengue.Infra.Secutiry.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiotecInfodengue.Infra.Secutiry.Services;

public class AuthorizationSecurity : IAuthorizationSecurity
{
    private readonly JwtSettings _jwtSettings;

    public AuthorizationSecurity(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string CreateToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            //Gravar os dados do usuário no token
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, usuario.Email),
                    new Claim(ClaimTypes.Role, "USER"),
                    new Claim(ClaimTypes.Role, "ADMI}N")
            }),
            //definindo a data e hora de expiração
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),

            //criptografar a chave antifalsificação no token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        };

        //retornando o token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}