using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPensanteApi.Extensions;
using SerPensanteApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;


namespace SerPensanteApi.Controllers;


[ApiController]
public class SubjectController : ControllerBase
{
    [HttpGet("v1/subjects")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var subjects = await context
            .Subjects
            .Include(x => x.Courses)
            .ToListAsync();
            return Ok(new ResultViewModel<List<Subject>>(subjects));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Subject>("ERMT001 - Falha interna no servior"));
        }
    }
    [HttpGet("v1/subject/science")]
    public async Task<IActionResult>GetByScienceAsync([FromServices] SpenDataContext context, [FromQuery] string science)
    {
        try
        {
            var subject = await context.Subjects.Where(x => x.Science == science).ToListAsync();
            return Ok(new ResultViewModel<List<Subject>>(subject));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Subject>("ERMT001 - Falha interna no servior"));
        }
    }

    [HttpGet("v1/subjects/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var subject = await context.Subjects.FirstOrDefaultAsync(x => x.Id == id);

            if (subject == null)
                return BadRequest(new ResultViewModel<Subject>("subject não encontrada, verifique os dados e tente novamente"));

            return Ok(new ResultViewModel<Subject>(subject));            
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Subject>("ERMT002 - Falha interna no servior"));
        }

    }

    [HttpPost("v1/subjects")]
    public async Task<IActionResult> PostAsync([FromBody] EditorSubjectViewModel model, [FromServices] SpenDataContext context)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Subject>(ModelState.GetErrors()));

        try
        {
            var subject = new Subject
            {
                Name = model.Name,
                Science = model.Science
            };
            await context.AddAsync(subject);
            await context.SaveChangesAsync();

            return Created($"subject/{subject.Id}", new ResultViewModel<Subject>(subject));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Subject>("Não foi possivel incluir a subject, verifique os dados e tente novamente."));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Subject>("ERRMT003 - Falha Interna do servidor"));
        }
    }

    [HttpPut("v1/subjects/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorSubjectViewModel model, [FromServices] SpenDataContext context)
    {
        try
        { 
            var subject = await context.Subjects.FirstOrDefaultAsync(x => x.Id == id);
            if (subject == null)
            {
                return NotFound(new ResultViewModel<Subject>("subject não encontrada"));
            }

            subject.Name = model.Name;
            subject.Science = model.Science;

            context.Subjects.Update(subject);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Subject>(subject));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Subject>("Não foi possivel atualizar a subject, verifique os dados e tente novamente"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Subject>("ERRMT004 - Falha interna no servidor"));
        }
    }

    [HttpDelete("v1/subjects/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var subject = await context.Subjects.FirstOrDefaultAsync(x => x.Id == id);

            if (subject == null)
            {
                return BadRequest(new ResultViewModel<Subject>("Não encontramos esta subject, verifique os dados e tente novamente"));
            }

            context.Subjects.Remove(subject);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Subject>(subject));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Subject>("ERRMT005 - Falha interna no servidor"));
        }
    }
}