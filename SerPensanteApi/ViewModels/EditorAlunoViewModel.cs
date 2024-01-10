using System.ComponentModel.DataAnnotations;

namespace SerPenApi.ViewModels; 

public class EditorAlunoViewModel
{
    [Required]
    [StringLength(60, MinimumLength = 4, ErrorMessage = "Minimo 4 caracteres")]
    public string Nome { get; set; }
    
    [Required]
    public DateTime Datanasc { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Minimo 8 Caracteres")]
    public string Telefone { get; set; }
   
   [Required]
   [EmailAddress]
   [RegularExpression(".+\\@.+\\..+",ErrorMessage = "Informe um email válido...")]
    public string Email { get; set; }
}