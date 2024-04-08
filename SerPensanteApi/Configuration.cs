namespace SerPensanteApi;

public static class Configuration
{
    // TOKEN
    public static string JwtKey { get; set; } = "QwTkdCV92URenHGK114ZpYyFc40";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}