using System.ComponentModel.DataAnnotations;

namespace SerPenApi.ViewModels;

public class CreateAulaViewModel : UpdateAulaViewModel
{
    [Required]
    public DateTime Duracao { get; set; }
    public int ProfessorId { get; set; }
    public string LinkUrl { get; set; }
    public int CursoId { get; set; }
}
