namespace SerPensanteApi.Models;

public class Materia{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Tipo { get; set; }

    public List<Curso> Cursos { get; set; }
}
