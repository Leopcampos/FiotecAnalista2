using FiotecInfodengue.Domain.Entities;

namespace FiotecInfodengue.Domain.Interfaces.Security;

public interface IAuthorizationSecurity
{
    string CreateToken(Usuario usuario);
}