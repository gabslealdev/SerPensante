namespace SerPensanteApi.Models;

public class StudentCourse 
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public int Progress { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}