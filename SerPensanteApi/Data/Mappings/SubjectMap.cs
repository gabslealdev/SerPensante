using Microsoft.EntityFrameworkCore;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class SubjectMap : IEntityTypeConfiguration<Subject>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Subject> builder)
    {
        // Criando tabela
        builder.ToTable("Subject");

        // Propriedades 
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1,1);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Science)
            .IsRequired()
            .HasColumnName("Science")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);
        
    }








    
}