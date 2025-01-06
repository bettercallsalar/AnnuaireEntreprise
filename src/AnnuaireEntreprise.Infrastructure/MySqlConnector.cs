// Author: Salar
// Created: 06/01/2024
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
        if (string.IsNullOrEmpty(_connectionString))
            throw new InvalidOperationException("The connection string is not configured properly.");
        return new MySqlConnection(_connectionString);
    }
}
