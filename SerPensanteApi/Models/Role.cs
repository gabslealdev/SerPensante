using System.Text.Json.Serialization;
using SerPensanteApi.Models;

namespace SerPenApi.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Teacher> TeacherRoles { get; set; }
    public IList<Student> StudentRoles { get; set; }
}