using Microsoft.EntityFrameworkCore;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class MateriaMap : IEntityTypeConfiguration<Materia>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Materia> builder)
    {
        // Criando tabela
        builder.ToTable("Materia");

        // Propriedades 
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1,1);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasColumnName("Tipo")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);
        
    }








    
}