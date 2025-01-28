using FPRWeb.Areas.Externo.Interface;
using FPRWeb.Areas.Externo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FPRWeb.Areas.Externo.Controllers
{
    [Area("Externo")]
    public class EquipoController:Controller
    {
        public IEquipoService _equipoService;
        public EquipoController(IEquipoService equipoService)
        { 
            _equipoService = equipoService;
        }
        public  IActionResult Equipo(int Id_Equipo) 
        {
            EquipoViewModel equipo = new EquipoViewModel();
            HorariosEntrenamientoModel horarios = new HorariosEntrenamientoModel();
            equipo.Horarios = horarios;
            return View(equipo);
        }
        [HttpPost]
        public async Task<IActionResult> Equipo(EquipoViewModel equipo)
        {
            equipo.Id_Equipo = await _equipoService.Equipo_Insertar(equipo, 1);
            return View(equipo);
        }
    }
}
