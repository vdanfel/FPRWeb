using Dapper;
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
        public async Task<bool> Equipo_Existe(string ruc, string nombre)
        {
            const string query = "SELECT 1 FROM Equipo where RUC = @RUC and Nombre = @Nombre";

            try
            {
                _connection.Open();
                var exists = await _connection.QueryFirstOrDefaultAsync<int>(query, new { RUC = ruc, Nombre = nombre});
                return exists == 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
