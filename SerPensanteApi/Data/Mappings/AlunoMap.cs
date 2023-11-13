using Microsoft.EntityFrameworkCore;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class AlunoMap : IEntityTypeConfiguration<Aluno>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Aluno"); 

        builder.HasKey(x => x.Matricula);

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
        
        builder.HasIndex(x => x.Email, "IX_ALUNO_EMAIL").IsUnique();
        
            
            


    }
}