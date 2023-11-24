using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class AlunoCursoMap : IEntityTypeConfiguration<AlunoCurso>
{
    public void Configure(EntityTypeBuilder<AlunoCurso> builder)
    {
        builder.ToTable("AlunoCurso");

        builder.HasKey(x => new {x.AlunoId, x.CursoId});

        builder.Property(x => x.CursoId)
            .IsRequired()
            .HasColumnName("CursoId");
        
        builder.Property(x => x.AlunoId)
            .IsRequired()
            .HasColumnName("AlunoId");
            

        builder.Property(x => x.DtInicio)
            .HasColumnName("DtIncio")
            .HasColumnType("Datetime");

        builder.Property(x => x.Dtfinal)
            .HasColumnName("DtFinal")
            .HasColumnType("Datetime");
        
        builder.Property(x => x.Progresso)
            .HasColumnName("Progresso")
            .HasColumnType("INT")
            .HasMaxLength(100);

        builder.HasOne(x => x.Aluno)
            .WithMany(x => x.AlunoCursos)
            .HasForeignKey(x => x.AlunoId)
            .HasConstraintName("FK_ALUNOCURSO_ALUNO")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Curso)
            .WithMany(x => x.AlunosCurso)
            .HasForeignKey(x => x.CursoId)
            .HasConstraintName("FK_ALUNOCURSO_CURSO")
            .OnDelete(DeleteBehavior.Cascade); 
            
        
        
        
    }
}