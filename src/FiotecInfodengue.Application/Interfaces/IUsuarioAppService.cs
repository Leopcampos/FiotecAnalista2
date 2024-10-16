using FiotecInfodengue.Application.Dtos;

namespace FiotecInfodengue.Application.Interfaces;

public interface IUsuarioAppService : IDisposable
{
    Task<IEnumerable<UsuarioDto>> GetAllAsync();
    Task<UsuarioDto> GetByEmailAsync(string email);
    Task<UsuarioDto> GetByIdAsync(string id);
    Task<UsuarioDto> CreateAsync(CriarUsuarioDto dto, string senha);
    Task<UsuarioDto> UpdateAsync(string id, AtualizarUsuarioDto dto);
    Task DeleteAsync(string id);
    Task<(string Token, string ErrorMessage)> AutenticarAsync(LoginDto loginDto);
}