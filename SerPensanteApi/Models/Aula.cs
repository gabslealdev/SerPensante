using System.Text.Json.Serialization;

namespace SerPensanteApi.Models;

public class Aula{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public DateTime Duracao { get; set; }
    public string LinkUrl { get; set; }
    public bool Assistido { get; set; }
    public int ProfessorId { get; set; }
    [JsonIgnore]
    public Professor Professor { get; set; }
    public int CursoId { get; set; }
    [JsonIgnore]
    public Curso Curso { get; set; }

}