using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FiotecInfodengue.Application.Dtos;

public class AtualizarUsuarioDto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [PasswordPropertyText]
    public string? Senha { get; set; }
}