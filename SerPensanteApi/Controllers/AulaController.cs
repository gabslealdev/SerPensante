using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPenApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;
using SerPenApi.Extensions;

namespace SerPenApi.Controllers;

[ApiController]
public class AulaController : ControllerBase
{
    [HttpGet("aulas")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var aulas = await context.Aulas.ToListAsync();
            return Ok(new ResultViewModel<List<Aula>>(aulas));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Aula>("Falha interna do servidor"));
        }
    }

    [HttpGet("aula/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var aula = await context.Aulas.FirstOrDefaultAsync(a => a.Id == id);

            if (aula == null)
                return BadRequest(new ResultViewModel<Aula>("Não foi possivel encontrar a aula requerida"));

            return Ok(new ResultViewModel<Aula>(aula));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Aula>("Falha interna no servidor"));
        }
    }

    [HttpPost("aulas")]
    public async Task<IActionResult> PostAsync([FromServices] SpenDataContext context, [FromBody] CreateAulaViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Aula>(ModelState.GetErrors()));
        try
        {
            var professor = await context.Professores.FirstOrDefaultAsync(p => p.Matricula == model.ProfessorId);
            var curso = await context.Cursos.FirstOrDefaultAsync(c => c.Id == model.CursoId);

            if (professor == null || curso == null)
                return BadRequest(new ResultViewModel<Aula>("Professor ou Curso não cadastrado"));

            var aula = new Aula
            {
                Titulo = model.Titulo,
                Duracao = model.Duracao,
                LinkUrl = model.LinkUrl,
                Professor = professor,
                ProfessorId = model.ProfessorId,
                Curso = curso,
                CursoId = model.CursoId
                
            };

            await context.Aulas.AddAsync(aula);
            await context.SaveChangesAsync();

            return Created($"aula/{aula.Id}", new ResultViewModel<Aula>(aula));


        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Aula>("Não foi possivel cadastrar essa aula"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Aula>("Falha interna no servidor"));
        }
    }

    [HttpPut("aula/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateAulaViewModel model, [FromServices] SpenDataContext context)
    {
        try
        {
            var aula = await context.Aulas.FirstOrDefaultAsync(a => a.Id == id);

            if (aula == null)
                return BadRequest(new ResultViewModel<Aula>("Aula não encontrada"));

            aula.Titulo = model.Titulo;

            context.Aulas.Update(aula);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Aula>(aula));

        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Aula>("Não foi possivel cadastrar essa aula"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Aula>("Falha interna no servidor"));
        }
    }

    [HttpDelete("aula/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var aula = await context.Aulas.FirstOrDefaultAsync(a => a.Id == id);

            if (aula == null)
                return BadRequest(new ResultViewModel<Aula>(aula));

            context.Aulas.Remove(aula);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Aula>(aula));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Aula>("Falha interna no servidor"));
        }
    }
}