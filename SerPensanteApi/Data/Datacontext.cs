using Microsoft.EntityFrameworkCore;
using SerPensanteApi.Data.Mappings;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data;

public class SpensanteDataContext : DbContext
{
    public DbSet<Materia> Materias { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<AlunoCurso> AlunosCursos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer("Server=localhost,1433;Database=SerPenDB;User ID=sa;Password=1q2w3e4r@#$;trustservercertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlunoMap()); 
        modelBuilder.ApplyConfiguration(new AulaMap());
        modelBuilder.ApplyConfiguration(new AlunoCursoMap());
        modelBuilder.ApplyConfiguration(new CursoMap());
        modelBuilder.ApplyConfiguration(new MateriaMap());
        modelBuilder.ApplyConfiguration(new ProfessorMap());
    }

}



