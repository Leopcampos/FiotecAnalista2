using FiotecInfodengue.Domain.Entities;
using FiotecInfodengue.Domain.Exceptions;
using FiotecInfodengue.Domain.Interfaces.Repositories;
using FiotecInfodengue.Domain.Interfaces.Services;

namespace FiotecInfodengue.Domain.Services;

public class UsuarioDomainService : IUsuarioDomainService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioDomainService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Usuario?> GetByIdAsync(int id)
        => await _repository.GetByIdAsync(id);

    public async Task<Usuario?> GetByEmailAsync(string email)
        => await _repository.GetByEmailAsync(email);

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        var usuarioExistente = await GetByEmailAsync(usuario.Email);
        if (usuarioExistente != null)
            throw new EmailException("Já existe um usuário com este e-mail.");

        return await _repository.CreateAsync(usuario);
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        return await _repository.UpdateAsync(usuario);
    }

    public async Task DeleteAsync(Usuario usuario)
    {
        await _repository.DeleteAsync(usuario);
    }

    public async Task<bool> CheckEmailAndSenhaAsync(string email, string senha)
    {
        var usuario = await GetByEmailAsync(email);
        if (usuario == null)
            return false;

        return usuario.Senha == senha;
    }

    public void Dispose()
        => _repository.Dispose();
}