using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AnnuaireEntreprise.Infrastructure;

public class MySqlConnector
{
    private readonly string _connectionString;
    // Constructor
    public MySqlConnector(IConfiguration configuration)
    {
        // Get the connection string from appsettings.json
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("The connection string is not configured properly.");
    }

    // Get a new connection
    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
