using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SerPensanteApi.Models;
using SerPensanteApi.Extensions;

namespace SerPensanteApi.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();         // Cria uma instância de tokenHandler(necessário para manipular o token). O tokenHandler espera um array de bites.
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);  // Pega a chave JwtKey(string) que está em Configuration.cs e converte para bytes 
        var claims = user.GetClaims();
        var tokenDescriptor = new SecurityTokenDescriptor         // Instância o item que contém todas das informações do token
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);    // Gerando token  
        return tokenHandler.WriteToken(token);                    // retorna uma string a partir do token gerado

    }
}