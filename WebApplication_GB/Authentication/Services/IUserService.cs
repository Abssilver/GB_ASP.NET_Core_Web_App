using Authentication.Responses;

namespace Authentication.Services
{
    public interface IUserService
    {
        TokenResponse Authenticate(string user, string password);
        string RefreshToken(string token);
    }
}