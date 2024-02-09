using System.Text.Json.Serialization;

namespace SerPensanteApi.Models;

public class Teacher : User {

    [JsonIgnore]
    public List<Lesson> Lessons { get; set; }
}