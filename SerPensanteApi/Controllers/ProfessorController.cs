using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPenApi.Extensions;
using SerPenApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;

namespace SerPenApi.Controllers;

[ApiController]
public class ProfessorController : ControllerBase
{
    [HttpGet("professores")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var professores = await context.Professores.ToListAsync();
            return Ok(new ResultViewModel<List<Professor>>(professores));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("Falha interna no servidor"));
        }

    }

    [HttpGet("professores/{matricula:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int matricula, [FromServices] SpenDataContext context)
    {
        try
        {
            var professor = await context.Professores.FirstOrDefaultAsync(x => x.Matricula == matricula);

            if (professor == null)
                return BadRequest(new ResultViewModel<Professor>("Não foi possivel encontrar este professor"));
            
            return Ok(new ResultViewModel<Professor>(professor));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("Falha interna no servidor"));
        }
    }

    [HttpPost("professores")]
    public async Task<IActionResult> PostAsync([FromBody] EditorProfessorViewModel model, [FromServices] SpenDataContext context)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Professor>(ModelState.GetErrors()));
        try
        {
            var professor = new Professor
            {
                Nome = model.Nome,
                Datanasc = model.DataNasc,
                Telefone = model.Telefone,
                Email = model.Email,
                PasswordHash = "teste",
                Imagem = "teste/teste/localhost"
            };
            await context.AddAsync(professor);
            await context.SaveChangesAsync();  

            return Created($"professor/{professor.Matricula}", new ResultViewModel<Professor>(professor));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Professor>("Não foi possivel cadastrar este professor"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("Falha interna do servidor"));
        }
    }

    [HttpPut("professores/{matricula:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int matricula, [FromBody] EditorProfessorViewModel model, [FromServices] SpenDataContext context)
    {
        try
        {
            var professor = await context.Professores.FirstOrDefaultAsync(p => p.Matricula == matricula);

            if(professor == null)
            {
                return BadRequest(new ResultViewModel<Professor>("Não foi possivel carregar as informações desse professor, verifique a matricula e tente novamente"));
            }

            professor.Nome = model.Nome;
            professor.Datanasc = model.DataNasc;
            professor.Telefone = model.Telefone; 
            professor.Email = model.Email;

            context.Professores.Update(professor);
            await context.SaveChangesAsync();
            
            return Ok(new ResultViewModel<Professor>(professor));
        }
        catch (DbUpdateException)
        {
            
            return BadRequest(new ResultViewModel<Professor>("Não foi possivel atualizar as informações desse professor"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("Falha interna no servidor"));
        }
    }

    [HttpDelete("professores/{matricula:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int matricula, [FromServices] SpenDataContext context)
    {
        try
        {
            var professor = await context.Professores.FirstOrDefaultAsync(p => p.Matricula == matricula);

            if(professor == null)
            {
                return BadRequest("Não foi possivel encontrar um professor com essa matricula");
            }

            context.Professores.Remove(professor);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Professor>(professor));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("Falha interna no servidor"));
        }

    }
}
