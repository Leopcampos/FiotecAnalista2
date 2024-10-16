using FiotecInfodengue.Domain.Entities;
using FiotecInfodengue.Infra.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FiotecInfodengue.Infra.Data.Context;

public class SqlContext : DbContext
{
    public SqlContext(DbContextOptions<SqlContext> option) : base(option)
    { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Perfil> Perfis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new PerfilConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
