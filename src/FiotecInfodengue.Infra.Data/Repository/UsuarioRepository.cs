using FiotecInfodengue.Domain.Entities;
using FiotecInfodengue.Domain.Interfaces.Repositories;
using FiotecInfodengue.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SqlContext _sqlContext;

    public UsuarioRepository(SqlContext sqlContext)
        => _sqlContext = sqlContext;

    public async Task<IEnumerable<Usuario>> GetAllAsync()
        => await _sqlContext.Usuarios.ToListAsync();

    public async Task<Usuario?> GetByEmailAsync(string email)
        => await _sqlContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<Usuario?> GetByIdAsync(int id)
        => await _sqlContext.Usuarios.FindAsync(id);

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        await _sqlContext.Usuarios.AddAsync(usuario);
        await _sqlContext.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        _sqlContext.Usuarios.Update(usuario);
        await _sqlContext.SaveChangesAsync();
        return usuario;
    }

    public async Task DeleteAsync(Usuario usuario)
    {
        _sqlContext.Usuarios.Remove(usuario);
        await _sqlContext.SaveChangesAsync();
    }

    public async Task<bool> CheckEmailAndSenhaAsync(string email, string senha)
    {
        var usuario = await _sqlContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario == null)
        {
            return false;
        }

        return usuario.Senha == senha;
    }

    public void Dispose()
        => _sqlContext.Dispose();
}