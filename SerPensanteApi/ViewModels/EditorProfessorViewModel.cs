using System.ComponentModel.DataAnnotations;

namespace SerPenApi.ViewModels;

public class EditorProfessorViewModel
{
   [Required]
   [StringLength(60, MinimumLength = 4, ErrorMessage = "Minimo 4 caracteres")]
   public string Nome { get; set; }
   [Required]
   public DateTime DataNasc { get; set; }

   [Required]
   [StringLength(20, MinimumLength = 8, ErrorMessage = "Minimo 8 Caracteres")]
   public string Telefone { get; set; }

   [Required]
   [DataType(DataType.EmailAddress, ErrorMessage = "Formato inválido")]
   [EmailAddress]
   public string Email { get; set; }
}