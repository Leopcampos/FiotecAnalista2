using FiotecInfodengue.Domain.Entities;

namespace FiotecInfodengue.Domain.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario> CreateAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task DeleteAsync(Usuario usuario);
    Task<bool> CheckEmailAndSenhaAsync(string email, string senha); 
    void Dispose();
}
