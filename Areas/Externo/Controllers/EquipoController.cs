using Microsoft.AspNetCore.Mvc;

namespace FPRWeb.Areas.Externo.Controllers
{
    [Area("Externo")]
    public class EquipoController:Controller
    {
        public IActionResult Equipo() 
        {
            return View();
        }
    }
}
