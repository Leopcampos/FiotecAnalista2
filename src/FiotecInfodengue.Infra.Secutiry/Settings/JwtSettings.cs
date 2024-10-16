namespace FiotecInfodengue.Infra.Secutiry.Settings;

public class JwtSettings
{
    public string? SecretKey { get; set; }
    public int ExpirationInMinutes { get; set; }
}