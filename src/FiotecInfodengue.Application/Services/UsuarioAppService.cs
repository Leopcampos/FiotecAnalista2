using AutoMapper;
using FiotecInfodengue.Application.Dtos;
using FiotecInfodengue.Application.Interfaces;
using FiotecInfodengue.Domain.Entities;
using FiotecInfodengue.Domain.Exceptions;
using FiotecInfodengue.Domain.Interfaces.Security;
using FiotecInfodengue.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace FiotecInfodengue.Application.Services;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioDomainService _usuarioDomainService;
    private readonly IAuthorizationSecurity _authorizationSecurity;
    private readonly IMapper _mapper;
    private readonly UserManager<Usuario> _userManager;

    public UsuarioAppService(IUsuarioDomainService usuarioDomainService,
        IMapper mapper,
        IAuthorizationSecurity authorizationSecurity,
        UserManager<Usuario> userManager)
    {
        _usuarioDomainService = usuarioDomainService;
        _mapper = mapper;
        _authorizationSecurity = authorizationSecurity;
        _userManager = userManager;
    }

    public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
    {
        var usuarios = await _usuarioDomainService.GetAllAsync();
        return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
    }

    public async Task<UsuarioDto> GetByEmailAsync(string email)
    {
        var usuario = await _usuarioDomainService.GetByEmailAsync(email);
        return _mapper.Map<UsuarioDto>(usuario);
    }

    public async Task<UsuarioDto> GetByIdAsync(string id)
    {
        var usuario = await _usuarioDomainService.GetByIdAsync(id);
        return _mapper.Map<UsuarioDto>(usuario);
    }

    public async Task<UsuarioDto> CreateAsync(CriarUsuarioDto dto, string senha)
    {
        try
        {
            var usuario = _mapper.Map<Usuario>(dto);
            usuario.DataCriacao = DateTime.Now;

            var result = await _userManager.CreateAsync(usuario, senha);

            if (!result.Succeeded)
            {
                throw new Exception("Erro ao criar o usuário.");
            }

            return _mapper.Map<UsuarioDto>(usuario);
        }
        catch (EmailException)
        {
            throw;
        }
    }

    public async Task<UsuarioDto> UpdateAsync(string id, AtualizarUsuarioDto dto)
    {
        try
        {
            var usuarioExistente = await _usuarioDomainService.GetByIdAsync(id);
            if (usuarioExistente == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            var usuario = _mapper.Map<Usuario>(dto);
            usuario.Id = id;

            await _usuarioDomainService.UpdateAsync(usuario);
            return _mapper.Map<UsuarioDto>(usuario);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteAsync(string id)
    {
        try
        {
            var usuario = await _usuarioDomainService.GetByIdAsync(id);
            await _usuarioDomainService.DeleteAsync(usuario);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<(string Token, string ErrorMessage)> AutenticarAsync(LoginDto loginDto)
    {
        try
        {
            var usuario = await _usuarioDomainService.GetByEmailAsync(loginDto.Email);

            if (usuario == null)
            {
                return (null, "Usuário não encontrado.");
            }

            // Verificar a senha usando o UserManager
            var validPassword = await _userManager.CheckPasswordAsync(usuario, loginDto.Senha);
            if (!validPassword)
            {
                return (null, "Senha inválida.");
            }

            var token = _authorizationSecurity.CreateToken(usuario);
            return (token, null);
        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }

    public void Dispose()
    {
        _usuarioDomainService.Dispose();
    }
}