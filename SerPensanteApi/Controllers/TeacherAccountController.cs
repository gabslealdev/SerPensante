using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SerPensanteApi.Extensions;
using SerPensanteApi.Models.Enums;
using SerPensanteApi.Services;
using SerPensanteApi.ViewModels.Accounts;
using SerPensanteApi.ViewModels;
using SerPensanteApi.Data;
using SerPensanteApi.Models;

namespace SerPensanteApi.Controllers;

[ApiController]
public class TeacherAccountController : ControllerBase
{
    [HttpGet("account/teachers")]
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

    [HttpGet("account/teachers/{id:int}")]
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

    [HttpPost("account/teachers")]
    public async Task<IActionResult> PostTeacherAsync([FromBody] EditorUserViewModel model, [FromServices] SpenDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Teacher>(ModelState.GetErrors()));

        var password = PasswordGenerator.Generate(length: 16, includeSpecialChars: true, upperCase: false);


        var teacher = new Teacher
        {
            Name = model.Name,
            BirthDate = model.BirthDate,
            Contact = model.Contact,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(password),
            Image = "src/profile/teacher/images",
            Role = Role.Teacher
        };

        try
        {
            await context.AddAsync(teacher);
            await context.SaveChangesAsync();

            return Created($"teacher/{teacher.Id}", new ResultViewModel<dynamic>(new
            {
                teacher = teacher.Email,
                password,
                teacher.Role
            }));
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


    [HttpPost("account/teacher/login")]
    public async Task<IActionResult> TeacherLogin([FromBody] LoginViewModel model, [FromServices] SpenDataContext context, [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Teacher>(ModelState.GetErrors()));


        var teacher = await context.Teachers
        .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (teacher == null)
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        if (!PasswordHasher.Verify(teacher.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        try
        {
            var token = tokenService.GenerateToken(teacher);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna do servidor"));
        }

    }

    [HttpPut("teachers/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorUserViewModel model, [FromServices] SpenDataContext context)
    {
        try
        {
            var Teacher = await context.Teachers
            .FirstOrDefaultAsync(p => p.Id == id);

            if (Teacher == null)
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


    [HttpDelete("account/teachers/remove/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] SpenDataContext context)
    {
        try
        {
            var teacher = await context.Teachers.FirstOrDefaultAsync(p => p.Id == id);

            if (teacher == null)
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