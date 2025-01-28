using FPRWeb.Areas.Externo.Models;

namespace FPRWeb.Areas.Externo.Interface
{
    public interface IEquipoService
    {
        Task<int> Equipo_Insertar(EquipoViewModel equipo, int usuario);
    }
}
