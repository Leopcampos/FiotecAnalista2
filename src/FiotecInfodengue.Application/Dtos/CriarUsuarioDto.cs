using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FiotecInfodengue.Application.Dtos;

public class CriarUsuarioDto
{
    [Required]
    [MinLength(8)]
    [MaxLength(100)]
    public string? Nome { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [PasswordPropertyText]
    public string? Senha { get; set; }
}