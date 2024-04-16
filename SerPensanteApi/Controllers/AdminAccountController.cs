using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SerPensanteApi.Data;
using SerPensanteApi.Extensions;
using SerPensanteApi.Models;
using SerPensanteApi.Models.Enums;
using SerPensanteApi.Services;
using SerPensanteApi.ViewModels.Accounts;
using SerPensanteApi.ViewModels;

namespace SerPensanteApi.Controllers;


[ApiController]
public class AdminAccountController : ControllerBase
{
    [Authorize(Roles = "Administrator")]
    [HttpPost("account/admin")]
    public async Task<IActionResult> PostAdminAsync([FromBody] EditorUserViewModel model, [FromServices] SpenDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

        var admin = new Administrator
        {
            Name = model.Name,
            BirthDate = model.BirthDate,
            Contact = model.Contact,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(model.Password),
            Image = " ",
            Role = Role.Administrator
        };

        try
        {
            await context.AddAsync(admin);
            context.SaveChanges();

            return Created($"admin/{admin.Id}", new ResultViewModel<dynamic>(new
            {
                admin = admin.Email,
                admin.Role,
                model.Password
            }));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<Student>("Não foi possivel cadastrar este perfil"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Student>("Falha interna do servidor"));
        }

    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("account/admin/login")]
    public async Task<IActionResult> AdminLogin([FromBody] LoginViewModel model, [FromServices] TokenService tokenService, [FromServices] SpenDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var admin = await context.Administrators.FirstOrDefaultAsync(x => x.Email == model.Email);
        
        if (admin == null)
            return BadRequest(new ResultViewModel<string>("Usuário e/ou senha inválidos"));

        if (!PasswordHasher.Verify(admin.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        try
        {
            var token = tokenService.GenerateToken(admin);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna do servidor"));
        }

    }
}