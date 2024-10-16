using FiotecInfodengue.Application.Dtos;

namespace FiotecInfodengue.Application.Interfaces;

public interface IUsuarioAppService : IDisposable
{
    Task<IEnumerable<UsuarioDto>> GetAllAsync();
    Task<UsuarioDto> GetByEmailAsync(string email);
    Task<UsuarioDto> GetByIdAsync(int id);
    Task<UsuarioDto> CreateAsync(CriarUsuarioDto dto);
    Task<UsuarioDto> UpdateAsync(int id, AtualizarUsuarioDto dto);
    Task DeleteAsync(int id);
    Task<(string Token, string ErrorMessage)> AutenticarAsync(LoginDto loginDto);
}