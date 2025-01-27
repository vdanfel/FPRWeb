using FPRWeb.Models.Login;
using FPRWeb.Results.Login;

namespace FPRWeb.Interface.Login
{
    public interface ILoginService
    {
        Task<LoginResult> ValidarLogin(LoginViewModel login);
    }
}
