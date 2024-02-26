using System.ComponentModel.DataAnnotations;

namespace SerPensanteApi.ViewModels.Accounts;

public class EditorUserViewModel
{
   [Required]
   [StringLength(60, MinimumLength = 4, ErrorMessage = "Minimo 4 caracteres")]
   public string Name { get; set; }
   [Required]
   public DateTime BirthDate { get; set; }

   [Required]
   [StringLength(20, MinimumLength = 8, ErrorMessage = "Minimo 8 Caracteres")]
   public string Contact { get; set; }

   [Required]
   [DataType(DataType.EmailAddress, ErrorMessage = "Formato inválido")]
   [EmailAddress]
   public string Email { get; set; }
}