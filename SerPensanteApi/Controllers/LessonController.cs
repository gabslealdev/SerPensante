using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPensanteApi.ViewModels.Lessons;
using SerPensanteApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;
using SerPensanteApi.Extensions;

namespace SerPenApi.Controllers;

[ApiController]
public class LessonController : ControllerBase
{
    [HttpGet("lesson")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var lesson = await context.Lessons.ToListAsync();
            return Ok(new ResultViewModel<List<Lesson>>(lesson));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Lesson>("Falha interna do servidor"));
        }
    }

    [HttpGet("lesson/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var lesson = await context.Lessons.FirstOrDefaultAsync(a => a.Id == id);

            if (lesson == null)
                return BadRequest(new ResultViewModel<Lesson>("Não foi possivel encontrar a Lesson requerida"));

            return Ok(new ResultViewModel<Lesson>(lesson));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Lesson>("Falha interna no servidor"));
        }
    }

    [HttpPost("lesson")]
    public async Task<IActionResult> PostAsync([FromServices] SpenDataContext context, [FromBody] CreateLessonViewModel model)
    {
        
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Lesson>(ModelState.GetErrors()));
        try
        {
            var teacher = await context.Teachers.FirstOrDefaultAsync(p => p.Id == model.TeacherId);
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == model.CourseId);

            if (teacher == null || course == null)
                return BadRequest(new ResultViewModel<Lesson>("Teacher ou Course não cadastrado"));

            var lesson = new Lesson
            {
                Title = model.Title,
                Duration = model.Duration,
                LinkUrl = model.LinkUrl,
                Teacher = teacher,
                TeacherId = model.TeacherId,
                Course = course,
                CourseId = model.CourseId
            };

            await context.Lessons.AddAsync(lesson);
            await context.SaveChangesAsync();

            return Created($"lesson/{lesson.Id}", new ResultViewModel<Lesson>(lesson));


        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Lesson>("Não foi possivel cadastrar essa Lesson"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Lesson>("Falha interna no servidor"));
        }
    }

    [HttpPut("lesson/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateLessonViewModel model, [FromServices] SpenDataContext context)
    {
        try
        {
            var lesson = await context.Lessons.FirstOrDefaultAsync(a => a.Id == id);

            if (lesson == null)
                return BadRequest(new ResultViewModel<Lesson>("Lesson não encontrada"));

            lesson.Title = model.Title;

            context.Lessons.Update(lesson);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Lesson>(lesson));

        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Lesson>("Não foi possivel cadastrar essa Lesson"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Lesson>("Falha interna no servidor"));
        }
    }

    [HttpDelete("lesson/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var lesson = await context.Lessons.FirstOrDefaultAsync(a => a.Id == id);

            if (lesson == null)
                return BadRequest(new ResultViewModel<Lesson>("Não foi possivel encontrar está aula"));

            context.Lessons.Remove(lesson);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Lesson>(lesson));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Lesson>("Falha interna no servidor"));
        }
    }
}