using Microsoft.AspNetCore.Mvc;

namespace FPRWeb.Areas.Interno.Controllers
{
    [Area("Interno")]
    public class JugadoresController:Controller
    {
        public IActionResult Jugadores() 
        {
            return View();
        }
    }
}
