using FiotecInfodengue.Domain.Entities;
using FiotecInfodengue.Domain.Interfaces.Repositories;
using FiotecInfodengue.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SqlContext _sqlContext;
    private readonly UserManager<Usuario> _userManager;

    public UsuarioRepository(SqlContext sqlContext, UserManager<Usuario> userManager)
    {
        _sqlContext = sqlContext;
        _userManager = userManager;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
        => await _sqlContext.Usuarios.ToListAsync();

    public async Task<Usuario?> GetByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    public async Task<Usuario?> GetByIdAsync(string id)
        => await _userManager.FindByIdAsync(id);

    public async Task<IdentityResult> CreateAsync(Usuario usuario, string senha)
        => await _userManager.CreateAsync(usuario, senha);

    public async Task<IdentityResult> UpdateAsync(Usuario usuario)
        => await _userManager.UpdateAsync(usuario);

    public async Task<IdentityResult> DeleteAsync(Usuario usuario)
        => await _userManager.DeleteAsync(usuario);

    public async Task<bool> CheckEmailAndSenhaAsync(string email, string senha)
    {
        var usuario = await _userManager.FindByEmailAsync(email);
        if (usuario == null)
        {
            return false;
        }

        return await _userManager.CheckPasswordAsync(usuario, senha);
    }

    public void Dispose()
        => _sqlContext.Dispose();
}