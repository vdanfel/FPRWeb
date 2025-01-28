using FPRWeb.Areas.Interno.Interface;
using Microsoft.Data.SqlClient;

namespace FPRWeb.Areas.Interno.Service
{
    public class JugadoresService:IJugadoresService
    {
        private readonly SqlConnection _connection;
        public JugadoresService(SqlConnection connection)
        { 
            _connection = connection;
        }
        public async Task<int> Persona_Insertar(JugadoresService)
    }
}
