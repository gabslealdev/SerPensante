using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPenApi.Extensions;
using SerPenApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;

namespace SerPenApi.Controller;

[ApiController]
public class CursoController : ControllerBase
{
    [HttpGet("cursos")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var cursos = await context.Cursos.ToListAsync();
            return Ok(new ResultViewModel<List<Curso>>(cursos));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Curso>("Falha interna no servidor"));
        }
    }
    [HttpGet("cursos/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var curso = await context.Cursos.FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null)
                return BadRequest(new ResultViewModel<Curso>("Não foi possivel encontrar este curso."));

            return Ok(new ResultViewModel<Curso>(curso));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Curso>("Falha interna no servidor"));
        }
    }

    [HttpPost("cursos")]
    public async Task<IActionResult> PostAsync([FromBody] EditorCursoViewModel model, [FromServices] SpenDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Curso>(ModelState.GetErrors()));
        try
        {
        var materia = await context.Materias.FirstOrDefaultAsync(m => m.Id == model.CodMateria);

        if (materia == null)
            return BadRequest(new ResultViewModel<Materia>("Não foi possivel encontrar a materia desse curso"));

        var _duracao = new DateTime(2024, 01, 01, 00, 00, 00);
        var curso = new Curso
        {
            Nome = model.Nome,
            Descricao = model.Descricao,
            LinkUrl = model.LinkUrl,
            Duracao = _duracao,
            CriadoEm = DateTime.UtcNow,
            CodMateria = model.CodMateria,
            Imagem = model.Imagem
        };

        await context.Cursos.AddAsync(curso);
        await context.SaveChangesAsync();

            return Created($"cursos/{curso.Id}", new ResultViewModel<Curso>(curso));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Curso>("Não foi possivel cadastrar este curso"));
        } 
        catch
        {
            return StatusCode(500, new ResultViewModel<Curso>("Falha interna do servidor"));
        }
    }

[HttpPut("cursos/{id:int}")]
public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCursoViewModel model, [FromServices] SpenDataContext context)
{
    try
    {
        var curso = await context.Cursos.FirstOrDefaultAsync(c => c.Id == id);

        if (curso == null)
            return BadRequest(new ResultViewModel<Curso>("Não foi possivel encontrar um curso com este id"));

        curso.Nome = model.Nome;
        curso.Descricao = model.Descricao;
        curso.LinkUrl = model.LinkUrl;
        curso.Imagem = model.Imagem;

        context.Cursos.Update(curso);
        await context.SaveChangesAsync();

        return Ok(new ResultViewModel<Curso>(curso));

    }
    catch (DbUpdateException)
    {
        return BadRequest(new ResultViewModel<Curso>("Não foi possivel atualizar este curso"));
    }
    catch
    {
        return StatusCode(500, new ResultViewModel<Curso>("Falha interna do servidor"));
    }
}

[HttpDelete("cursos/{id:int}")]
public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] SpenDataContext context)
{
    try
    {
        var curso = await context.Cursos.FirstOrDefaultAsync(c => c.Id == id);

        if (curso == null)
            return BadRequest(new ResultViewModel<Curso>("Não foi possivel encontrar um curso com este id"));

        context.Cursos.Remove(curso);
        await context.SaveChangesAsync();

        return Ok(new ResultViewModel<Curso>(curso));
    }
    catch
    {
        return StatusCode(500, new ResultViewModel<Curso>("Falha interna do servidor"));
    }
}  
}