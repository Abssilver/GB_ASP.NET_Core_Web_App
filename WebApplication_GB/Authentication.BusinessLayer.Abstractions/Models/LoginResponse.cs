namespace Authentication.BusinessLayer.Abstractions.Models
{
    public sealed class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}