using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => !x.Excluido);

            builder.Property(x => x.CPF)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(x => x.NomeCompleto)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.ToTable("Usuarios");
        }
    }
}
