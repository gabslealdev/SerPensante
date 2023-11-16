using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class AulaMap : IEntityTypeConfiguration<Aula>
{
    public void Configure(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("Materia");

        builder.HasKey(x => x.Id);
        // CODE-FIRST builder.Property(x => x.Id).ValueGeneratorOnAdd().UseIdenityColumn();

        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasColumnName("Titulo")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.Duracao)
            .IsRequired()
            .HasColumnName("Duracao")
            .HasColumnType("DATETIME");

        builder.Property(x => x.LinkUrl)
            .HasColumnName("linkURL")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80); 

        builder.HasIndex(x => x.Titulo, "IX_AULA_TITULO").IsUnique();  

        builder.HasOne(x => x.professor)
            .WithMany(x => x.Aulas)
            .HasConstraintName("FK_PROFESSOR")
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.curso)
            .WithMany(x => x.Aulas)
            .HasConstraintName("FK_CURSO")
            .OnDelete(DeleteBehavior.NoAction);
    }
}