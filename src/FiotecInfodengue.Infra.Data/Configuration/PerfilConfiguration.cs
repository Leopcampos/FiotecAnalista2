using FiotecInfodengue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiotecInfodengue.Infra.Data.Configuration;

public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Role).IsRequired().HasMaxLength(20).HasColumnName("ROLE");
        builder.Property(x => x.UsuarioId).IsRequired().HasColumnName("USUARIO_ID");
        builder.HasAlternateKey(x => x.UsuarioId);
    }
}