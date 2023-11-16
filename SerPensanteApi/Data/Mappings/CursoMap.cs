using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class CursoMap : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("Materia");

        builder.HasKey(x => x.Id);
        // CODE-FIRST builder.Property(x => x.Id).ValueGeneratorOnAdd().UseIdenityColumn();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Duracao)
            .IsRequired()
            .HasColumnName("Duracao")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnName("Descricao")
            .HasColumnType("TEXT");
        
        builder.Property(x => x.LinkUrl)
            .HasColumnName("linkURL")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Ativo)
            .HasColumnType("BIT")
            .HasColumnName("Ativo")
            .HasDefaultValue(0);
        
        builder.Property(x => x.CriadoEm)
            .IsRequired()
            .HasColumnName("CriadoEm")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.Imagem)
            .HasColumnName("Imagem")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        builder.HasIndex(x => x.Nome, "IX_CURSO_NOME").IsUnique();

        builder.HasOne(x => x.materia)
            .WithMany(x => x.Cursos)
            .HasConstraintName("FK_MATERIA")
            .OnDelete(DeleteBehavior.NoAction);
    

    }
}