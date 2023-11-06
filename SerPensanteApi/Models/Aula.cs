using System;

namespace SerPensante.Models;

class Aula{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public DateTime Duracao { get; set; }
    public string LinkUrl { get; set; }
    public int ProfessorId { get; set; }
    public int CursoId { get; set; }

}