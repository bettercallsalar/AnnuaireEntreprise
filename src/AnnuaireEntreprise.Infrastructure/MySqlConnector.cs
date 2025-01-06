using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AnnuaireEntreprise.Infrastructure;

public class MySqlConnector
{
    private readonly string _connectionString;

    public MySqlConnector(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("The connection string is not configured properly.");
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
