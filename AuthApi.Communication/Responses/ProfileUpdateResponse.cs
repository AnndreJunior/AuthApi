namespace AuthApi.Communication.Responses;

public class ProfileUpdateResponse
{
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
}
