using System.Text.Json.Serialization;

namespace SerPensanteApi.Models;

public class Lesson{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Duration { get; set; }
    public string LinkUrl { get; set; }
    public bool Watched { get; set; }
    public int TeacherId { get; set; }
    [JsonIgnore]
    public Teacher Teacher { get; set; }
    public int CourseId { get; set; }
    [JsonIgnore]
    public Course Course { get; set; }

}