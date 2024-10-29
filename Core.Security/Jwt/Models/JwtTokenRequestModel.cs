namespace Academy.AuthenticationService.Model;

public class JwtTokenRequestModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}