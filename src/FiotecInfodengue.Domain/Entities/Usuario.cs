using Microsoft.AspNetCore.Identity;

namespace FiotecInfodengue.Domain.Entities;

public class Usuario : IdentityUser
{
    public string? Nome { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public Perfil Perfil { get; set; }
}