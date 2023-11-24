using Microsoft.EntityFrameworkCore;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data;

public class SpensanteDataContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<Professor> Professores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer("Server=localhost,1433;Database=spdatabase;User ID=sa;Password=1q2w3e4r@#$");
    
}



