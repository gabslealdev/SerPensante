using System.ComponentModel.DataAnnotations;


namespace SerPensanteApi.ViewModels.Lessons; 

public class UpdateLessonViewModel
{
    [Required]
    [StringLength(60, MinimumLength = 4, ErrorMessage = "Minimo 4 caracteres")]
    public string Title { get; set; }
}


