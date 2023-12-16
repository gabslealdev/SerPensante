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
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Materia>("Falha interna no servior"));
        }
    }

    [HttpGet("materia/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var materia = await context.Materias.FirstOrDefaultAsync(x => x.Id == id);

            if (materia == null)
                {
                    return NotFound();
                }

            return Ok(new ResultViewModel<Materia>(materia));            
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Materia>("Falha interna no servior"));
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

            return Created($"materias/{materia.Id}", materia);
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<List<string>>("Não foi possivel incluir a materia"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<string>>("Falha Interna do servidor"));
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
                return NotFound();
            }

            materia.Nome = model.Nome;
            materia.Tipo = model.Tipo;

            context.Materias.Update(materia);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Materia>(materia));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Materia>("Não foi possivel atualizar a materia"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Materia>("Falha interna no servidor"));
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
                return NotFound();
            }

            context.Materias.Remove(materia);
            await context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Materia>("Falha interna no servidor"));
        }
    }
}