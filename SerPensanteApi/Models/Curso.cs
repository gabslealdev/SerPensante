using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace SerPensanteApi.Models;

public class Curso
{

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string LinkUrl { get; set; }
    public bool Ativo { get; set; }
    public int CodMateria { get; set; }
    [JsonIgnore]
    public Materia Materia { get; set; }
    public string Imagem { get; set; }
    [JsonIgnore]
    public List<Aula> Aulas { get; set; }
    public ICollection<AlunoCurso> AlunosCurso { get; set; }
    private DateTime _duracao;
    public DateTime Duracao { get; set; }
    public DateTime CriadoEm { get; set; }

    public DateTime DuracaoTotal()
    {
        if (Aulas.Count == 0)
        {
            _duracao = new DateTime(2024, 01, 01, 00, 00, 00);
            return _duracao;
        }

        foreach (Aula aula in Aulas)
        {
            var hours = aula.Duracao.ToOADate();
            _duracao.AddHours(hours);
        }
        return _duracao;
    }

}