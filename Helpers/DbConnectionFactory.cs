using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace Library_Management_System.Helpers
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }

}
