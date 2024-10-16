using FiotecInfodengue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiotecInfodengue.Infra.Data.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(x => x.Id).HasName("ID");
        builder.Property(x => x.Nome).IsRequired().HasMaxLength(100).HasColumnName("NOME");
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50).HasColumnName("EMAIL");
        builder.Property(x => x.DataCriacao).HasColumnName("DATA_CRIACAO");

        builder.HasIndex(x => x.Email).IsUnique();
    }
}