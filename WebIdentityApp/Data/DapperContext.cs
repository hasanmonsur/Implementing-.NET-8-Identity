using Npgsql;
using System.Data;

namespace WebIdentityApp.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateDbConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
