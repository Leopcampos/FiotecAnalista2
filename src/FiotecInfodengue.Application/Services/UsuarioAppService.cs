using AutoMapper;
using FiotecInfodengue.Application.Dtos;
using FiotecInfodengue.Application.Interfaces;
using FiotecInfodengue.Domain.Entities;
using FiotecInfodengue.Domain.Exceptions;
using FiotecInfodengue.Domain.Interfaces.Security;
using FiotecInfodengue.Domain.Interfaces.Services;

namespace FiotecInfodengue.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IAuthorizationSecurity _authorizationSecurity;
        private readonly IMapper _mapper;

        public UsuarioAppService(IUsuarioDomainService usuarioDomainService,
            IAuthorizationSecurity authorizationSecurity,
            IMapper mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _authorizationSecurity = authorizationSecurity;
            _mapper = mapper;
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

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            var usuario = await _usuarioDomainService.GetByIdAsync(id);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> CreateAsync(CriarUsuarioDto dto)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(dto);
                usuario.DataCriacao = DateTime.Now;

                var usuarioCriado = await _usuarioDomainService.CreateAsync(usuario);

                return _mapper.Map<UsuarioDto>(usuarioCriado);
            }
            catch (EmailException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o usuário: " + ex.Message);
            }
        }

        public async Task<UsuarioDto> UpdateAsync(int id, AtualizarUsuarioDto dto)
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
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o usuário: " + ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var usuario = await _usuarioDomainService.GetByIdAsync(id);
                await _usuarioDomainService.DeleteAsync(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar o usuário: " + ex.Message);
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

                // Verificar a senha
                var validPassword = await _usuarioDomainService.CheckEmailAndSenhaAsync(loginDto.Email, loginDto.Senha);
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
}