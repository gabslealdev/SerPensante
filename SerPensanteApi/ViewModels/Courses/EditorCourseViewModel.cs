using System.ComponentModel.DataAnnotations;

namespace SerPensanteApi.ViewModels.Courses;

public class EditorCourseViewModel
{
    [Required(ErrorMessage = " Atenção! 'Nome' é um campo obrigatório.")]
    public string Name { get; set; }
    
    [Required (ErrorMessage = "Atenção! 'Descrição' é um campo obrigatório.")]
    public string Description { get; set; }
        
    [Required(ErrorMessage = "Atenção! 'Link Url' é um campo obrigatório.")]
    public string LinkUrl { get; set; }
    
    [Required(ErrorMessage = "Atenção! 'Materia' é um campo obrigatório.")]
    public int SubjectId { get; set; }
    
    [Required(ErrorMessage = "Atenção! 'Imagem' é um campo obrigatório.")]
    public string Image { get; set; }
}