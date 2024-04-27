using System.ComponentModel.DataAnnotations;

namespace SerPensanteApi.ViewModels.Lessons;

public class UploadLessonViewModel
{
    [Required(ErrorMessage = "Videoaula inválida")]
    public string Base64Lesson { get; set; }
}