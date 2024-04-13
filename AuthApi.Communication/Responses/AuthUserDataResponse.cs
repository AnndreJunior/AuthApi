namespace AuthApi.Communication.Responses;

public class AuthUserDataResponse
{
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public string? Bio { get; set; }
}
