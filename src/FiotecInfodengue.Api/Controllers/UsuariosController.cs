using FiotecInfodengue.Application.Dtos;
using FiotecInfodengue.Application.Interfaces;
using FiotecInfodengue.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiotecInfodengue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuariosController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        /// <summary>
        /// Buscar todos os usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet("buscar-todos")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioAppService.GetAllAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Buscar usuário por Id
        /// </summary>
        [HttpGet("buscar-por-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioAppService.GetByIdAsync(id);
            return usuario != null ? Ok(usuario) : NotFound();
        }

        /// <summary>
        /// Buscar usuário por email
        /// </summary>
        [HttpGet("buscar-por-email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var usuario = await _usuarioAppService.GetByEmailAsync(email);
            return usuario != null ? Ok(usuario) : NotFound();
        }

        /// <summary>
        /// Criar usuário
        /// </summary>
        [HttpPost("criar-usuario")]
        public async Task<IActionResult> Create([FromBody] CriarUsuarioDto dto)
        {
            try
            {
                var usuario = await _usuarioAppService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
            }
            catch (EmailException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Atualizar usuário
        /// </summary>
        [HttpPut("atualizar-usuario/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarUsuarioDto dto)
        {
            try
            {
                var usuario = await _usuarioAppService.UpdateAsync(id, dto);
                return Ok(usuario);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletar usuário
        /// </summary>
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("deletar-usuario/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _usuarioAppService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}