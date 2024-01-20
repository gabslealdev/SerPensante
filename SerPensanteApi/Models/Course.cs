using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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
    [JsonIgnore]
    public List<Lesson> Lessons { get; set; }
    public ICollection<StudentCourse> StudentsCourse { get; set; }
    private DateTime _duration;
    public DateTime Duration { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime DuracaoTotal()
    {
        if (Lessons.Count == 0)
        {
            _duration = new DateTime(2024, 01, 01, 00, 00, 00);
            return _duration;
        }

        foreach (Lesson lesson in Lessons)
        {
            var hours = lesson.Duration.ToOADate();
            _duration.AddHours(hours);
        }
        return _duration;
    }

}