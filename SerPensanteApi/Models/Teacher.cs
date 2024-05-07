using System.Text.Json.Serialization;

namespace SerPensanteApi.Models;

public class Teacher : User {
    public List<Lesson> Lessons { get; set; }
}