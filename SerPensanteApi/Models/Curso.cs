namespace SerPensanteApi.Models;

public class Curso{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime Duracao { get; set; }
    public string Descricao { get; set; }
    public string LinkUrl { get; set; }
    public bool Ativo { get; set; }
    public DateTime CriadoEm { get; set; }
    public Materia materia { get; set; }
    public string Imagem { get; set; }
    public ICollection<AlunoCurso> AlunosCurso { get; set; }
}