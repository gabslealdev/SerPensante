using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class CourseMap : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

        builder.HasKey(x => x.Id);
        // CODE-FIRST builder.Property(x => x.Id).ValueGeneratorOnAdd().UseIdenityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Duration)
            .IsRequired()
            .HasColumnName("Duration")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnName("Description")
            .HasColumnType("TEXT");
        
        builder.Property(x => x.LinkUrl)
            .HasColumnName("linkURL")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Active)
            .HasColumnType("BIT")
            .HasColumnName("Active")
            .HasDefaultValue(0);
        
        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME");

        builder.Property(x => x.SubjectId)
            .IsRequired()
            .HasColumnName("SubjectId")
            .HasColumnType("INT");
        
        builder.Property(x => x.Image)
            .HasColumnName("Image")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        builder.HasIndex(x => x.Name, "IX_COURSE_NAME").IsUnique();

        builder.HasOne(x => x.Subject)
             .WithMany(x => x.Courses)
             .HasForeignKey(x => x.SubjectId)
             .HasConstraintName("FK_SUBJECT")
             .OnDelete(DeleteBehavior.NoAction);
    }
}