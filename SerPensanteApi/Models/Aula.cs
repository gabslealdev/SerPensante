using System;

namespace SerPensanteApi.Models;

public class Aula{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public DateTime Duracao { get; set; }
    public string LinkUrl { get; set; }
    public Professor professor { get; set; }
    public Curso curso { get; set; }

}