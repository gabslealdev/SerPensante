using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SerPensanteApi.Extensions;
using SerPensanteApi.Models.Enums;
using SerPensanteApi.Services;
using SerPensanteApi.ViewModels;
using SerPensanteApi.ViewModels.Accounts;
using SerPensanteApi.Data;
using SerPensanteApi.Models;
using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;

namespace SerPensanteApi.Controllers;


[ApiController]
public class AccountController : ControllerBase
{
    [HttpGet("v1/account/students")]
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

    [HttpGet("v1/account/students/{id:int}")]
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
    [AllowAnonymous]
    [HttpPost("v1/account/students")]
    public async Task<IActionResult> PostStudentsAsync([FromBody] EditorUserViewModel model, [FromServices] SpenDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Student>(ModelState.GetErrors()));

        var student = new Student
        {
            Name = model.Name,
            BirthDate = model.BirthDate,
            Contact = model.Contact,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(model.Password),
            Image = "",
            Role = Role.Student
        };
        try
        {
            await context.AddAsync(student);
            context.SaveChanges();

            return Created($"student/{student.Id}", new ResultViewModel<dynamic>(new
            {
                student = student.Email,
                student.Role,
                model.Password
            }));
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

    [AllowAnonymous]
    [HttpPost("v1/account/students/login")]
    public async Task<IActionResult> StudentLogin([FromBody] LoginViewModel model, [FromServices] SpenDataContext context, [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Teacher>(ModelState.GetErrors()));


        var student = await context.Students
        .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (student == null)
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        if (!PasswordHasher.Verify(student.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        try
        {
            var token = tokenService.GenerateToken(student);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna do servidor"));
        }

    }
    [Authorize(Roles = "Student")]
    [HttpPut("v1/account/students/update/{id:int}")]
    public async Task<IActionResult> PutAsync([FromBody] EditorUserViewModel model, [FromRoute] int id, [FromServices] SpenDataContext context)
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

    [HttpDelete("v1/account/students/remove/{id:int}")]
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
    [Authorize(Roles = "Student")]
    [HttpPost("v1/accounts/students/upload-image")]
    public async Task<IActionResult> UploadImage([FromBody] UploadImageViewModel model, [FromServices] SpenDataContext context)
    {
        var fileName = Guid.NewGuid().ToString() + ".jpg";

        var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(model.Base64Image, "");

        var imageBytes = Convert.FromBase64String(data);

        var blobclient = new BlobClient("DefaultEndpointsProtocol=https;AccountName=serpensante01;AccountKey=IoTaTXxpx1VcmRxqoFyjbdrz87MnxmIrVvte+JokDt2JQ1MrxgmKEXhrw+4iPsLu+BEJ0bOzvGDf+AStAAEBcQ==;EndpointSuffix=core.windows.net", "source", fileName);

        try
        {
            using (var stream = new MemoryStream(imageBytes))
            {
                blobclient.Upload(stream);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
        }

        var student = await context.Students.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

        if (student == null)
            return NotFound(new ResultViewModel<string>("Usuário não encontrado"));

        student.Image = blobclient.Uri.AbsoluteUri;
        try
        {
            context.Students.Update(student);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
        }

        return Ok(new ResultViewModel<string>("Imagem alterada com sucesso!", null));
    }
}