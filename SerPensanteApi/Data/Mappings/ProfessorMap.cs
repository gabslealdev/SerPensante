using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class ProfessorMap : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        // Criando tabela
        builder.ToTable("Professor"); 

        // Propriedades
        builder.HasKey(x => x.Matricula);
        
        builder.Property(x => x.Matricula)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1,1);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(60);

        builder.Property(x => x.Datanasc)
            .IsRequired()
            .HasColumnName("Datanasc")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.Telefone)
            .IsRequired()
            .HasColumnName("Telefone")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Ativo)
            .HasColumnType("BIT")
            .HasColumnName("Ativo")
            .HasDefaultValue(0);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.Imagem)
            .HasColumnName("Imagem")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
        
        //Index
        builder.HasIndex(x => x.Email, "IX_PROFESSOR_EMAIL").IsUnique();

    }
}