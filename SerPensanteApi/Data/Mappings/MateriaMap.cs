using Microsoft.EntityFrameworkCore;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class MateriaMap : IEntityTypeConfiguration<Materia>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Materia> builder)
    {
        // Gerar tabela
        builder.ToTable("Materia");

        // Gerando chave primaria
        builder.HasKey(x => x.Id);
        // CODE-FIRST builder.Property(x => x.Id).ValueGeneratorOnAdd().UseIdenityColumn();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasColumnName("Tipo")
            .HasColumnType("TINYINT")
            .HasMaxLength(20);
    }








    
}