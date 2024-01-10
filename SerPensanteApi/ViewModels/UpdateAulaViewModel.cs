using System.ComponentModel.DataAnnotations;


namespace SerPenApi.ViewModels; 

public class UpdateAulaViewModel
{
    [Required]
    [StringLength(60, MinimumLength = 4, ErrorMessage = "Minimo 4 caracteres")]
    public string Titulo { get; set; }
}


