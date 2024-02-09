using SerPensanteApi.Models.Enums;

namespace SerPensanteApi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Contact { get; set; }
    public string  Email { get; set; }
    public bool Active { get; set; }
    public string PasswordHash { get; set; }
    public string Image { get; set; } 
    public Role Role { get; set; }
}