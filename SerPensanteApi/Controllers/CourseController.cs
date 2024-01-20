using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPenApi.Extensions;
using SerPenApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;

namespace SerPenApi.Controller;

[ApiController]
public class CourseController : ControllerBase
{
    [HttpGet("courses")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var courses = await context.Courses.ToListAsync();
            return Ok(new ResultViewModel<List<Course>>(courses));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Course>("Falha interna no servidor"));
        }
    }
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

            var _duration = new DateTime(2024, 01, 01, 00, 00, 00);
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
}