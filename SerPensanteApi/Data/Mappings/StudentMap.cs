using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class StudentMap : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        // Criando tabela
        builder.ToTable("Student"); 

        // Propriedades
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1,1);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(60);

        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasColumnName("BirthDate")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.Contact)
            .IsRequired()
            .HasColumnName("Contact")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Active)
            .HasColumnType("BIT")
            .HasColumnName("Active")
            .HasDefaultValue(0);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.Image)
            .HasColumnName("Image")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
        
        builder.HasOne(x => x.Role)
            .WithMany(x => x.StudentRoles)
            .HasConstraintName("FK_STUDENT_ROLE")
            .OnDelete(DeleteBehavior.Cascade);

        // Index
        builder.HasIndex(x => x.Email, "IX_STUDENT_EMAIL").IsUnique();

    }
}