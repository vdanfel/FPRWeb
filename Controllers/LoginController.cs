using FPRWeb.Interface.Login;
using FPRWeb.Models.Login;
using Microsoft.AspNetCore.Mvc;

namespace FPRWeb.Controllers
{
    public class LoginController:Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        { 
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Login(string mensaje)
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            string mensaje = "";
            var usuario = await _loginService.ValidarLogin(login);
            if (usuario != null)
            {
                if (usuario.Id_011_TipoUsuario == 409)
                {
                    return RedirectToAction("Equipo", "Equipo", new { area = "Externo" });
                }
                else if (usuario.Id_011_TipoUsuario == 407)
                {
                    return RedirectToAction("Jugadores", "Jugadores", new { area = "Interno" });

                }
            }
            mensaje = "Usuario o Clave incorrecto";
            ViewBag.Mensaje = mensaje;
            return View(login);
        }
    }
}
