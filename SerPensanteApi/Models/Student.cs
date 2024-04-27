using SerPensanteApi.Models;

namespace SerPensanteApi.Models;

public class Student : User
{
    public List<StudentCourse> StudentCourses { get; set; }
}