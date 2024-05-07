using System.Text.Json.Serialization;

namespace SerPensanteApi.Models;

public class Course
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LinkUrl { get; set; }
    public bool Active { get; set; }
    public int SubjectId { get; set; }
    [JsonIgnore]
    public Subject Subject { get; set; }
    public string Image { get; set; }
    public List<Lesson> Lessons { get; set; }
    public ICollection<StudentCourse> StudentsCourse { get; set; }
    public DateTime Duration { get; set; }
    public DateTime CreatedAt { get; set; }


}