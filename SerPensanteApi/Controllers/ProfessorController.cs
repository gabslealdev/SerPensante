using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPenApi.Extensions;
using SerPenApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;

namespace SerPenApi.Controllers;

[ApiController]
public class TeacherController : ControllerBase
{
    [HttpGet("teachers")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var teachers = await context.Teachers.ToListAsync();
            return Ok(new ResultViewModel<List<Teacher>>(teachers));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Teacher>("Falha interna no servidor"));
        }

    }

    [HttpGet("teachers/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.Id == id);

            if (teacher == null)
                return BadRequest(new ResultViewModel<Teacher>("Não foi possivel encontrar este Teacher"));
            
            return Ok(new ResultViewModel<Teacher>(teacher));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Teacher>("Falha interna no servidor"));
        }
    }

    [HttpPost("teachers")]
    public async Task<IActionResult> PostAsync([FromBody] EditorTeacherViewModel model, [FromServices] SpenDataContext context)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Teacher>(ModelState.GetErrors()));
        try
        {
            var teacher = new Teacher
            {
                Name = model.Name,
                BirthDate = model.BirthDate,
                Contact = model.Contact,
                Email = model.Email,
                PasswordHash = "teste",
                Image = "teste/teste/localhost"
            };
            await context.AddAsync(teacher);
            await context.SaveChangesAsync();  

            return Created($"Teacher/{teacher.Id}", new ResultViewModel<Teacher>(teacher));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Teacher>("Não foi possivel cadastrar este Teacher"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Teacher>("Falha interna do servidor"));
        }
    }

    [HttpPut("teachers/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorTeacherViewModel model, [FromServices] SpenDataContext context)
    {
        try
        {
            var Teacher = await context.Teachers.FirstOrDefaultAsync(p => p.Id == id);

            if(Teacher == null)
            {
                return BadRequest(new ResultViewModel<Teacher>("Não foi possivel carregar as informações desse Teacher, verifique a id e tente novamente"));
            }

            Teacher.Name = model.Name;
            Teacher.BirthDate = model.BirthDate;
            Teacher.Contact = model.Contact; 
            Teacher.Email = model.Email;

            context.Teachers.Update(Teacher);
            await context.SaveChangesAsync();
            
            return Ok(new ResultViewModel<Teacher>(Teacher));
        }
        catch (DbUpdateException)
        {
            
            return BadRequest(new ResultViewModel<Teacher>("Não foi possivel atualizar as informações desse Teacher"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Teacher>("Falha interna no servidor"));
        }
    }

    [HttpDelete("teachers/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var teacher = await context.Teachers.FirstOrDefaultAsync(p => p.Id == id);

            if(teacher == null)
            {
                return BadRequest("Não foi possivel encontrar um Teacher com essa id");
            }

            context.Teachers.Remove(teacher);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Teacher>(teacher));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Teacher>("Falha interna no servidor"));
        }

    }
}
