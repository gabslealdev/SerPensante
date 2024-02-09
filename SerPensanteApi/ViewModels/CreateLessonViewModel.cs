using System.ComponentModel.DataAnnotations;

namespace SerPensanteApi.ViewModels;

public class CreateLessonViewModel : UpdateLessonViewModel
{
    [Required]
    public DateTime Duration { get; set; }
    public int TeacherId { get; set; }
    public string LinkUrl { get; set; }
    public int CourseId { get; set; }
}
