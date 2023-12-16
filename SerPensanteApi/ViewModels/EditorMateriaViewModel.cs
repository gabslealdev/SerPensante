using System.ComponentModel.DataAnnotations;

namespace SerPenApi.ViewModels;

public class EditorMateriaViewModel
{
    [Required(ErrorMessage = "O nome da materia é obrigatorio")]
    [StringLength(40, MinimumLength = 4, ErrorMessage = "Minimo 4 letras")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O tipo da materia é obrigatorio")]
    [StringLength(40, MinimumLength = 4, ErrorMessage = "Minimo 4 letras")]
    public string Tipo { get; set; }
}