using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPenApi.Extensions;
using SerPenApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;

namespace SerPenApi.Controllers;

[ApiController]
public class AlunoController : ControllerBase
{
    [HttpGet("alunos")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var alunos = await context.Alunos.ToListAsync();
            return Ok(new ResultViewModel<List<Aluno>>(alunos));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Aluno>("Falha interna no servidor"));
        }
    }

    [HttpGet("alunos/{matricula:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int matricula, [FromServices] SpenDataContext context)
    {
        try
        {
            var aluno = await context.Alunos.FirstOrDefaultAsync(x => x.Matricula == matricula);

            if (aluno == null)
                return BadRequest(new ResultViewModel<Aluno>("O Aluno com esta matricula não foi encontrado"));

            return Ok(new ResultViewModel<Aluno>(aluno));

        }

        catch
        {
            return StatusCode(500, new ResultViewModel<Aluno>("Falha interna no servidor"));
        }
    }

    [HttpPost("alunos")]
    public async Task<IActionResult> PostAsync([FromBody] EditorAlunoViewModel model, [FromServices] SpenDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Professor>(ModelState.GetErrors()));
        try
        {
            var aluno = new Aluno
            {
                Nome = model.Nome,
                Datanasc = model.Datanasc,
                Telefone = model.Telefone,
                Email = model.Email,
                PasswordHash = "null",
                Imagem = "null"
            };

            await context.AddAsync(aluno);
            context.SaveChanges();

            return Created($"aluno/{aluno.Matricula}", new ResultViewModel<Aluno>(aluno));

        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Aluno>("Não foi possivel cadastrar este Aluno"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Aluno>("Falha interna do servidor"));
        }
    }

    [HttpPut("alunos/{matricula:int}")]
    public async Task<IActionResult> PutAsync([FromBody] EditorAlunoViewModel model, [FromRoute] int matricula, [FromServices] SpenDataContext context)
    {
        try
        {
            var aluno = await context.Alunos.FirstOrDefaultAsync(x => x.Matricula == matricula);

            if (aluno == null)
            {
                return BadRequest(new ResultViewModel<Aluno>("O aluno não foi encontrado ou a matricula está incorreta"));
            }

            aluno.Nome = model.Nome;
            aluno.Telefone = model.Telefone;
            aluno.Datanasc = model.Datanasc;
            aluno.Email = model.Email;

            context.Alunos.Update(aluno);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Aluno>(aluno));

        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Aluno>("Não foi possivel atualizar este Aluno"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Aluno>("Falha interna do servidor"));
        }
    }
    [HttpDelete("alunos/{matricula:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int matricula, [FromServices] SpenDataContext context)
    {
        try
        {
            var aluno = await context.Alunos.FirstOrDefaultAsync(x => x.Matricula == matricula);

            if (aluno == null)
            {
                return BadRequest(new ResultViewModel<Aluno>("Não foi possivel deletar este Aluno"));
            }

            context.Alunos.Remove(aluno);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Aluno>(aluno));

        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("Falha interna no servidor"));
        }

    }
}