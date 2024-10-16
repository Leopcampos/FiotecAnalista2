using System.ComponentModel.DataAnnotations.Schema;

namespace FiotecInfodengue.Domain.Entities;

public class Perfil
{
    public int Id { get; set; }
    public string? Role { get; set; }
    public int UsuarioId { get; set; }
    public List<Usuario?> Usuarios { get; set; }
}