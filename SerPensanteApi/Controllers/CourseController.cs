using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPensanteApi.Extensions;
using SerPensanteApi.ViewModels;
using SerPensanteApi.ViewModels.Courses;
using SerPensanteApi.Data;
using SerPensanteApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace SerPenApi.Controllers;

[ApiController]
public class CourseController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("courses")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context,[FromQuery] int pageSize = 25, [FromQuery] int page = 0)
    {
        try
        {
            var count = await context.Courses.AsNoTracking().CountAsync();
            var courses = await context
            .Courses
            .AsNoTracking()
            .Include(x => x.Lessons)
            .ThenInclude(x => x.Teacher)
            .Select(x => new ListCoursesViewModel{
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                Lessons = x.Lessons
            })
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
            return Ok(new ResultViewModel<dynamic>(new {
                total = count,
                page,
                pageSize,
                courses
            }));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Course>("Falha interna no servidor"));
        }
    }

    [HttpGet("courses/subject/{subject}")]
    public async Task<IActionResult> GetSubjectsCourseAsync([FromServices] SpenDataContext context, [FromRoute] string subject)
    {
        var courses = await context.Courses.Where(x => x.Subject.Name == subject).AsNoTracking().ToListAsync();
        return Ok(new ResultViewModel<List<Course>>(courses));
    }

    [AllowAnonymous]
    [HttpGet("courses/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var courses = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (courses == null)
                return BadRequest(new ResultViewModel<Course>("Não foi possivel encontrar este Course."));

            return Ok(new ResultViewModel<Course>(courses));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Course>("Falha interna no servidor"));
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("courses")]
    public async Task<IActionResult> PostAsync([FromBody] EditorCourseViewModel model, [FromServices] SpenDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Course>(ModelState.GetErrors()));
        try
        {
            var materia = await context.Subjects.FirstOrDefaultAsync(m => m.Id == model.SubjectId);

            if (materia == null)
                return BadRequest(new ResultViewModel<Subject>("Não foi possivel encontrar a materia desse Course"));

            var _duration = new DateTime(1900, 01, 01, 00, 00, 00);
            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                LinkUrl = model.LinkUrl,
                Duration = _duration,
                CreatedAt = DateTime.UtcNow,
                SubjectId = model.SubjectId,
                Image = model.Image
            };

            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();

            return Created($"Courses/{course.Id}", new ResultViewModel<Course>(course));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Course>("Não foi possivel cadastrar este Course"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Course>("Falha interna do servidor"));
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("courses/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCourseViewModel model, [FromServices] SpenDataContext context)
    {
        try
        {
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return BadRequest(new ResultViewModel<Course>("Não foi possivel encontrar um Course com este id"));

            course.Name = model.Name;
            course.Description = model.Description;
            course.LinkUrl = model.LinkUrl;
            course.Image = model.Image;

            context.Courses.Update(course);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Course>(course));

        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Course>("Não foi possivel atualizar este Course"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Course>("Falha interna do servidor"));
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("courses/{id:int}")] 
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return BadRequest(new ResultViewModel<Course>("Não foi possivel encontrar um Course com este id"));

            context.Courses.Remove(course);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Course>(course));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Course>("Falha interna do servidor"));
        }
    }

    [Authorize(Roles = "Student")]
    [HttpPost("enrollment/courses")]
    public async Task<IActionResult> EnrollmentCourse([FromServices] SpenDataContext context, [FromQuery] int id )
    {
        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        var student = await context.Students.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

        if (student == null || course == null)
            return BadRequest();

        
        var studentCourse = new StudentCourse 
        {
            Student = student,
            StudentId = student.Id,
            Course = course,
            CourseId = course.Id,
            Progress = 0,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow,
        };

        try
        {
            await context.AddAsync(studentCourse);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(studentCourse)); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<dynamic>(ex));
        }          
    }
}