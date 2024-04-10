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
using System.Text.RegularExpressions;
using Azure.Storage.Blobs;

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
    public async Task<IActionResult> PostTeacherAsync([FromBody] EditorUserViewModel model,[FromServices] SpenDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Teacher>(ModelState.GetErrors()));

        var teacher = new Teacher
        {
            Name = model.Name,
            BirthDate = model.BirthDate,
            Contact = model.Contact,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(model.Password),
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
                model.Password,
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


    [HttpPost("account/teachers/login")]
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

    [HttpPut("account/teachers/update/{id:int}")]
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

    [HttpPost("account/teachers/upload-image")]
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

        var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

        if (teacher == null)
            return BadRequest(new ResultViewModel<string>("Usuário não encontrado"));

        teacher.Image = blobclient.Uri.AbsoluteUri;
        try
        {
            context.Teachers.Update(teacher);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
        }
        return Ok(new ResultViewModel<string>("Imagem alterada com sucesso!", null));
    }
}