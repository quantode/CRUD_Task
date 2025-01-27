using System.Data;
using Microsoft.Data.SqlClient;

namespace CRUD_Task.DBContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connection;
        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("DefaultConnection", "Connection string 'DefaultConnection' is not configured.");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connection);

    }
}
