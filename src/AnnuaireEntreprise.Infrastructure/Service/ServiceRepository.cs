// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace AnnuaireEntreprise.Infrastructure
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly MySqlConnector _connector;

        public ServiceRepository(MySqlConnector connector)
        {
            _connector = connector;
        }

        public async Task<List<ServiceDTO>> FetchAllServices()
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Services";
            using var reader = await command.ExecuteReaderAsync();
            var services = new List<ServiceDTO>();
            while (await reader.ReadAsync())
            {
                services.Add(new ServiceDTO
                {
                    Id = reader.GetInt32("Id"),
                    Nom = reader.GetString("Nom")
                });
            }
            return services ?? new List<ServiceDTO>();
        }

        public async Task<ServiceDTO> FetchServiceById(int id)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Services WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@id", id));
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                return new ServiceDTO
                {
                    Id = reader.GetInt32("Id"),
                    Nom = reader.GetString("Nom")
                };
            }
            return new ServiceDTO
            {
                Id = 0,
                Nom = string.Empty
            };
        }

        public async Task<bool> InsertService(ServiceDTO service)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Services (Nom) VALUES (@nom)";
            command.Parameters.Add(new MySqlParameter("@nom", service.Nom));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateService(ServiceDTO service)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Services SET Nom = @nom WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@nom", service.Nom));
            command.Parameters.Add(new MySqlParameter("@id", service.Id));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteService(ServiceDTO service)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Services WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@id", service.Id));
            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}