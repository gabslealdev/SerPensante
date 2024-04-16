using System.Text.Json.Serialization;

namespace SerPensanteApi.Models;

public class Subject{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Science { get; set; }
    public List<Course> Courses { get; set; }
}
