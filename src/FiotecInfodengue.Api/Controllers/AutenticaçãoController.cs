using AutoMapper;
using FiotecInfodengue.Application.Dtos;
using FiotecInfodengue.Application.Interfaces;
using FiotecInfodengue.Domain.Interfaces.Security;
using Microsoft.AspNetCore.Mvc;

namespace FiotecInfodengue.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutenticaçãoController : ControllerBase
{
    private readonly IAuthorizationSecurity _authorizationSecurity;
    private readonly IUsuarioAppService _usuarioAppService;
    private readonly IMapper _mapper;

    public AutenticaçãoController(IAuthorizationSecurity authorizationSecurity, IMapper mapper, IUsuarioAppService usuarioAppService)
    {
        _authorizationSecurity = authorizationSecurity;
        _mapper = mapper;
        _usuarioAppService = usuarioAppService;
    }

    [HttpPost("autenticar")]
    public async Task<IActionResult> Autenticar([FromBody] LoginDto dto)
    {
        var (token, errorMessage) = await _usuarioAppService.AutenticarAsync(dto);
        return token != null ? Ok(new { Token = token }) : Unauthorized(errorMessage);
    }
}