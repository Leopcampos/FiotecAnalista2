using FiotecInfodengue.Domain.Entities;

namespace FiotecInfodengue.Domain.Interfaces.Services;

public interface IUsuarioDomainService : IDisposable
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario> CreateAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task DeleteAsync(Usuario usuario);
    Task<bool> CheckEmailAndSenhaAsync(string email, string senha);
}