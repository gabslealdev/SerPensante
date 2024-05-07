using System.Text.Json.Serialization;

namespace SerPensanteApi.Models;

public class Lesson{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Duration { get; set; }
    [JsonIgnore]
    public string LinkUrl { get; set; }
    [JsonIgnore]
    public bool Watched { get; set; }
    [JsonIgnore]
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    [JsonIgnore]
    public int CourseId { get; set; }
    public Course Course { get; set; }

}