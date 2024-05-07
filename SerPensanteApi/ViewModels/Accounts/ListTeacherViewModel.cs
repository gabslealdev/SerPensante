using SerPensanteApi.Models;

namespace SerPensanteApi.ViewModels.Accounts;

public class ListTeacherViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Lesson> Lessons { get; set; }
}