using Microsoft.EntityFrameworkCore;
using SerPenApi.Data.Mappings;
using SerPensanteApi.Data.Mappings;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data;

public class SpenDataContext : DbContext
{
    public SpenDataContext(DbContextOptions<SpenDataContext> options) : base(options)
    {
    }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<StudentCourse> StudentSCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentMap()); 
        modelBuilder.ApplyConfiguration(new LessonMap());
        modelBuilder.ApplyConfiguration(new StudentCourseMap());
        modelBuilder.ApplyConfiguration(new CourseMap());
        modelBuilder.ApplyConfiguration(new SubjectMap());
        modelBuilder.ApplyConfiguration(new TeacherMap());
        modelBuilder.ApplyConfiguration(new AdministratorMap());
    }
}



