using System.ComponentModel.DataAnnotations;

namespace SerPensanteApi.ViewModels;

public class EditorSubjectViewModel
{
    [Required(ErrorMessage = "O nome da materia é obrigatorio")]
    [StringLength(40, MinimumLength = 4, ErrorMessage = "Minimo 4 letras")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O tipo da materia é obrigatorio")]
    [StringLength(40, MinimumLength = 4, ErrorMessage = "Minimo 4 letras")]
    public string Science { get; set; }
}