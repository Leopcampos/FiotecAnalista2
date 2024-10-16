using FiotecInfodengue.Domain.Entities;
using FiotecInfodengue.Domain.Exceptions;
using FiotecInfodengue.Domain.Interfaces.Repositories;
using FiotecInfodengue.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace FiotecInfodengue.Domain.Services;

public class UsuarioDomainService : IUsuarioDomainService
{
    private readonly IUsuarioRepository _repository;
    private readonly UserManager<Usuario> _userManager;

    public UsuarioDomainService(IUsuarioRepository repository, UserManager<Usuario> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Usuario?> GetByIdAsync(string id)
        => await _repository.GetByIdAsync(id);

    public async Task<Usuario?> GetByEmailAsync(string email)
        => await _repository.GetByEmailAsync(email);

    public async Task<IdentityResult> CreateAsync(Usuario usuario, string senha)
    {
        var usuarioExistente = await GetByEmailAsync(usuario.Email);
        if (usuarioExistente != null)
            throw new EmailException("Já existe um usuário com este e-mail.");

        return await _userManager.CreateAsync(usuario, senha);
    }

    public async Task<IdentityResult> UpdateAsync(Usuario usuario)
    {
        return await _repository.UpdateAsync(usuario);
    }

    public async Task<IdentityResult> DeleteAsync(Usuario usuario)
    {
        return await _repository.DeleteAsync(usuario);
    }

    public async Task<bool> CheckEmailAndSenhaAsync(string email, string senha)
    {
        var usuario = await GetByEmailAsync(email);
        if (usuario == null)
            return false;

        return await _userManager.CheckPasswordAsync(usuario, senha);
    }

    public void Dispose()
        => _repository.Dispose();
}