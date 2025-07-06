namespace OrderGeneratorApi.Infra.Settings;

public class RabbitMqSettings
{
    public string HostName { get; set; } = string.Empty;
    public int Port { get; set; } = 0;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public bool IsValid()
    {
        
        return !string.IsNullOrEmpty(HostName) &&
               Port > 0 &&
               !string.IsNullOrEmpty(UserName) &&
               !string.IsNullOrEmpty(Password);
    }
}