﻿using FPRWeb.Areas.Externo.Models;

namespace FPRWeb.Areas.Externo.Interface
{
    public interface IEquipoService
    {
        Task<int> Equipo_Insertar(EquipoViewModel equipo, int usuario);
        Task<int> Equipo_Existe(string nombre, string ruc);
        Task Equipo_Actualizar(EquipoViewModel equipo, int Usuario);
        Task<bool> Archivo_Existe(int idEquipo, int idJugador, int idTipoArchivo);
        Task Archivo_Insertar(int Id_Equipo, int Id_Jugador, int Id_013_TipoArchivo, string RutaArchivo, int Usuario);
    }
}
