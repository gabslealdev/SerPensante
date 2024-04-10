using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SerPensanteApi.ViewModels.Accounts;

public class EditorUserViewModel
{
   [Required (ErrorMessage = "Nome é um campo obrigatório.")]
   [StringLength(60, MinimumLength = 4, ErrorMessage = "Minimo 4 caracteres")]
   public string Name { get; set; }
   [Required (ErrorMessage = "Data de nascimento é um campo obrigatório.")]
   public DateTime BirthDate { get; set; }

   [Required (ErrorMessage = "Telefone é um campo obrigatório")]
   [StringLength(20, MinimumLength = 8, ErrorMessage = "Minimo 8 Caracteres")]
   public string Contact { get; set; }

   [Required (ErrorMessage = "Email é um campo obrigatório")]
   [DataType(DataType.EmailAddress, ErrorMessage = "Formato inválido")]
   [EmailAddress]
   public string Email { get; set; }

   [Required]
   [PasswordPropertyText]
   public string Password { get; set; }
   
}