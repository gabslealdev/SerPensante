using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPenApi.Extensions;
using SerPenApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;


namespace SerPensanteApi.Controllers;


[ApiController]
public class MateriaController : ControllerBase
{
    [HttpGet("materias")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var materias = await context.Materias.ToListAsync();
            return Ok(new ResultViewModel<List<Materia>>(materias));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Materia>("ERMT001 - Falha interna no servior"));
        }
    }

    [HttpGet("materias/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var materia = await context.Materias.FirstOrDefaultAsync(x => x.Id == id);

            if (materia == null)
                return BadRequest(new ResultViewModel<Materia>("Materia não encontrada, verifique os dados e tente novamente"));

            return Ok(new ResultViewModel<Materia>(materia));            
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Materia>("ERMT002 - Falha interna no servior"));
        }

    }

    [HttpPost("materias")]
    public async Task<IActionResult> PostAsync([FromBody] EditorMateriaViewModel model, [FromServices] SpenDataContext context)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Materia>(ModelState.GetErrors()));

        try
        {
            var materia = new Materia
            {
                Nome = model.Nome,
                Tipo = model.Tipo
            };
            await context.AddAsync(materia);
            await context.SaveChangesAsync();

            return Created($"materia/{materia.Id}", new ResultViewModel<Materia>(materia));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Materia>("Não foi possivel incluir a materia, verifique os dados e tente novamente."));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Materia>("ERRMT003 - Falha Interna do servidor"));
        }
    }

    [HttpPut("materias/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorMateriaViewModel model, [FromServices] SpenDataContext context)
    {
        try
        { 
            var materia = await context.Materias.FirstOrDefaultAsync(x => x.Id == id);
            if (materia == null)
            {
                return NotFound(new ResultViewModel<Materia>("Materia não encontrada"));
            }

            materia.Nome = model.Nome;
            materia.Tipo = model.Tipo;

            context.Materias.Update(materia);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Materia>(materia));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Materia>("Não foi possivel atualizar a materia, verifique os dados e tente novamente"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Materia>("ERRMT004 - Falha interna no servidor"));
        }
    }

    [HttpDelete("materias/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var materia = await context.Materias.FirstOrDefaultAsync(x => x.Id == id);

            if (materia == null)
            {
                return BadRequest(new ResultViewModel<Materia>("Não encontramos esta materia, verifique os dados e tente novamente"));
            }

            context.Materias.Remove(materia);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Materia>(materia));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Materia>("ERRMT005 - Falha interna no servidor"));
        }
    }
}