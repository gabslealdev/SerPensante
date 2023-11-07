namespace SerPensanteApi.Models;

public class Aluno {
    public int Matricula { get; set; }
    public string Nome { get; set; }
    public DateTime Datanasc { get; set; }
    public string Telefone { get; set; }
    public string  Email { get; set; }
    public bool Ativo { get; set; }
    public string PasswordHash { get; set; }

}