using SerPensanteApi.Models;

namespace SerPensanteApi.Models;

public class Student : User
{
    public ICollection<StudentCourse> StudentCourses { get; set; }
}