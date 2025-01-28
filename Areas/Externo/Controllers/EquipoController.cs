using FPRWeb.Areas.Externo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FPRWeb.Areas.Externo.Controllers
{
    [Area("Externo")]
    public class EquipoController:Controller
    {
        public  IActionResult Equipo(int Id_Equipo) 
        {
            EquipoViewModel equipo = new EquipoViewModel();
            HorariosEntrenamientoModel horarios = new HorariosEntrenamientoModel();
            equipo.Horarios = horarios;
            return View(equipo);
        }
    }
}
