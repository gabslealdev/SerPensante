using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerPenApi.Extensions;
using SerPenApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;

namespace SerPenApi.Controllers;

[ApiController]
public class StudentController : ControllerBase
{
    [HttpGet("students")]
    public async Task<IActionResult> GetAsync([FromServices] SpenDataContext context)
    {
        try
        {
            var students = await context.Students.ToListAsync();
            return Ok(new ResultViewModel<List<Student>>(students));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Student>("Falha interna no servidor"));
        }
    }

    [HttpGet("students/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
                return BadRequest(new ResultViewModel<Student>("O Student com esta matricula não foi encontrado"));

            return Ok(new ResultViewModel<Student>(student));

        }

        catch
        {
            return StatusCode(500, new ResultViewModel<Student>("Falha interna no servidor"));
        }
    }

    [HttpPost("students")]
    public async Task<IActionResult> PostAsync([FromBody] EditorStudentViewModel model, [FromServices] SpenDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Student>(ModelState.GetErrors()));
        try
        {
            var student = new Student
            {
                Name = model.Name,
                BirthDate = model.BirthDate,
                Contact = model.Contact,
                Email = model.Email,
                PasswordHash = "teste",
                Image = "teste"
            };

            await context.AddAsync(student);
            context.SaveChanges();

            return Created($"student/{student.Id}", new ResultViewModel<Student>(student));

        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Student>("Não foi possivel cadastrar este Student"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Student>("Falha interna do servidor"));
        }
    }

    [HttpPut("students/{id:int}")]
    public async Task<IActionResult> PutAsync([FromBody] EditorStudentViewModel model, [FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return BadRequest(new ResultViewModel<Student>("O Student não foi encontrado ou a matricula está incorreta"));
            }

            student.Name = model.Name;
            student.Contact = model.Contact;
            student.BirthDate = model.BirthDate;
            student.Email = model.Email;

            context.Students.Update(student);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Student>(student));

        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Student>("Não foi possivel atualizar este Student"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Student>("Falha interna do servidor"));
        }
    }
    [HttpDelete("students/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return BadRequest(new ResultViewModel<Student>("Não foi possivel deletar este Student"));
            }

            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Student>(student));

        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Student>("Falha interna no servidor"));
        }

    }
}