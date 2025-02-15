﻿using Dapper;
using FPRWeb.Areas.Externo.Interface;
using FPRWeb.Areas.Externo.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FPRWeb.Areas.Externo.Service
{
    public class EquipoService:IEquipoService
    {
        public readonly SqlConnection _connection;
        public EquipoService(SqlConnection connection) 
        {
            _connection = connection;
        }
        public async Task<int> Equipo_Insertar(EquipoViewModel equipo, int usuario)
        {
            var procedure = "usp_Equipo_Insert";
            try 
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", equipo.Nombre);
                parameters.Add("@Siglas", equipo.Siglas);
                parameters.Add("@RUC", equipo.RUC);
                parameters.Add("@RazonSocial", equipo.RazonSocial);
                parameters.Add("@RepresentanteLegal", equipo.RepresentanteLegal);
                parameters.Add("@UsuarioAdministrativo", equipo.UsuarioAdministrativo);
                parameters.Add("@Contacto", equipo.Contacto);
                parameters.Add("@LugarEntrenamiento", equipo.LugarEntrenamiento);
                parameters.Add("@Usuario", usuario);
                
                var idEquipo = await _connection.QuerySingleAsync<int>(
                procedure,
                parameters,
                commandType: CommandType.StoredProcedure
                );

                return idEquipo;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado. Intenta nuevamente.", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public async Task Equipo_Actualizar(EquipoViewModel equipo, int Usuario)
        {
            var procedure = "usp_Archivos_Insert";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id_Equipo", equipo.Id_Equipo);
                parameters.Add("@Nombre", equipo.Nombre);
                parameters.Add("@Siglas", equipo.Siglas);
                parameters.Add("@RUC", equipo.RUC);
                parameters.Add("@RazonSocial", equipo.RazonSocial);
                parameters.Add("@RepresentanteLegal", equipo.RepresentanteLegal);
                parameters.Add("@UsuarioAdministrativo", equipo.UsuarioAdministrativo);
                parameters.Add("@Contacto", equipo.Contacto);
                parameters.Add("@LugarEntrenamiento", equipo.LugarEntrenamiento);
                parameters.Add("@Usuario", Usuario);
                await _connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado. Intenta nuevamente.", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public async Task<int> Equipo_Existe(string nombre, string ruc)
        {
            var procedure = "usp_Equipo_Existe";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", nombre);
                parameters.Add("@RUC", ruc);

                var existe = await _connection.QuerySingleAsync<int>(
                procedure,
                parameters,
                commandType: CommandType.StoredProcedure
                );

                return existe;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado. Intenta nuevamente.", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public async Task Archivo_Insertar(int Id_Equipo,int Id_Jugador,int Id_013_TipoArchivo,string RutaArchivo,int Usuario)
        {
            var procedure = "usp_Archivos_Insert";
            try
            { 
                var parameters = new DynamicParameters();
                parameters.Add("@Id_Equipo", Id_Equipo);
                parameters.Add("@Id_Jugador", Id_Jugador);
                parameters.Add("@Id_013_TipoArchivo", Id_013_TipoArchivo);
                parameters.Add("@RutaArchivo", RutaArchivo);
                parameters.Add("@Usuario", Usuario);
                await _connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado. Intenta nuevamente.", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public async Task<bool> Archivo_Existe(int idEquipo, int idJugador, int idTipoArchivo)
        {
            var procedure = "usp_Archivos_Exist";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id_Equipo", idEquipo);
                parameters.Add("@Id_Jugador", idJugador); // Si aplica, pasa el Id_Jugador (si no se usa, puedes quitarlo)
                parameters.Add("@Id_013_TipoArchivo", idTipoArchivo); // Aquí puedes adaptar este parámetro según sea necesario

                var result = await _connection.QuerySingleOrDefaultAsync<int>(
                    procedure,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result > 0; // Si devuelve un Id de archivo, significa que existe
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al verificar si el archivo existe.", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        //public async Task<int> Archivo_Existe(int Id_Equipo, int Id_Jugador, int Id_013_TipoArchivo)
        //{
        //    const string query = "SELECT Id_Archivos FROM Archivos where Id_Equipo = @Id_Equipo and Id_Jugador = @Id_Jugador and Id_013_TipoArchivo = @Id_013_TipoArchivo";

        //    try
        //    {
        //        _connection.Open();
        //        var Id_Archivo = await _connection.QueryFirstOrDefaultAsync<int>(query, new { Id_Equipo = Id_Equipo, Id_Jugador = Id_Jugador , Id_013_TipoArchivo = Id_013_TipoArchivo });
        //        return Id_Archivo;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        _connection.Close();
        //    }
        //}

    }
}
