using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPensanteApi.ViewModels.Lessons;
using SerPensanteApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;
using SerPensanteApi.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace SerPenApi.Controllers;

[ApiController]
public class LessonController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("lessons")]
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

    [AllowAnonymous]
    [HttpGet("lessons/{id:int}")]
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

    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Teacher")]
    [HttpPost("lessons")]
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

            long totalTicks = course.Duration.Ticks + model.Duration.Ticks;
            var totalSpan = new TimeSpan(totalTicks);
            course.Duration = new DateTime(1900, 01, 01, 00, 00, 00).Add(totalSpan); 

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

            
            context.Courses.Update(course);
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

    [Authorize(Roles = "Administrator")]
    [HttpPut("lessons/{id:int}")]
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

    [Authorize(Roles = "Administrator")]
    [HttpDelete("course={courseid}/lesson={lessonid:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int lessonid, [FromRoute] int courseid, [FromServices] SpenDataContext context)
    {
        try
        {
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == courseid);
            var lesson = await context.Lessons.FirstOrDefaultAsync(a => a.Id == lessonid);

            if (lesson == null || course == null)
                return BadRequest(new ResultViewModel<Lesson>("Não foi possivel encontrar está aula"));

            long totalTicks = course.Duration.Ticks - lesson.Duration.Ticks;
            var totalSpan = new TimeSpan(totalTicks);
            course.Duration = new DateTime(1900, 1, 1, 0, 0, 0).Add(totalSpan);

            context.Courses.Update(course);
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