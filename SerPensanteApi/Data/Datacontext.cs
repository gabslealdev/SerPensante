using Microsoft.EntityFrameworkCore;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data;

public class SpensanteDataContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<AlunoCurso> alunoCursos  { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Role> Roles { get; set; }
}

