using System.Text.Json.Serialization;
using SerPenApi.Models;

namespace SerPensanteApi.Models;

public class Teacher : User {

    [JsonIgnore]
    public List<Lesson> Lessons { get; set; }
}