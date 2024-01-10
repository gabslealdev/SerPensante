namespace SerPensanteApi.Models;

public class AlunoCurso 
{
    public int AlunoId { get; set; }
    public Aluno Aluno { get; set; }
    public int CursoId { get; set; }
    public Curso Curso { get; set; }
    public int Progresso { get; set; }
    public DateTime DtInicio { get; set; }
    public DateTime Dtfinal { get; set; }
}