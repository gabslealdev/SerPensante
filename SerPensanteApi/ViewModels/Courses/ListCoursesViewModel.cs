using SerPensanteApi.Models;

namespace SerPensanteApi.ViewModels.Courses;

public class ListCoursesViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Lesson> Lessons { get; set; }
}