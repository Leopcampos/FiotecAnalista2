using FiotecInfodengue.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FiotecInfodengue.Domain.Interfaces.Repositories;

public interface IUsuarioRepository : IDisposable
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(string id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<IdentityResult> CreateAsync(Usuario usuario, string senha);
    Task<IdentityResult> UpdateAsync(Usuario usuario);
    Task<IdentityResult> DeleteAsync(Usuario usuario);
    Task<bool> CheckEmailAndSenhaAsync(string email, string senha);
}