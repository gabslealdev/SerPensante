using System.Text.Json;

namespace SerPensanteApi.Models;

public class Aula{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public DateTime Duracao { get; set; }
    public string LinkUrl { get; set; }
    public bool Assistido { get; set; }
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; }
    public int CursoId { get; set; }
    public Curso Curso { get; set; }

}