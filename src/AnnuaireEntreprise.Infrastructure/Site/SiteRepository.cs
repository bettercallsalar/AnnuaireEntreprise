// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace AnnuaireEntreprise.Infrastructure
{
    public class SiteRepository : ISiteRepository
    {
        private readonly MySqlConnector _connector;

        public SiteRepository(MySqlConnector connector)
        {
            _connector = connector;
        }
        public async Task<List<SiteDTO>> GetAllSites()
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Sites";
            using var reader = await command.ExecuteReaderAsync();
            var sites = new List<SiteDTO>();
            while (await reader.ReadAsync())
            {
                sites.Add(new SiteDTO
                {
                    Id = reader.GetInt32("Id"),
                    Ville = reader.GetString("Ville")
                });
            }
            return sites ?? new List<SiteDTO>();
        }

        public async Task<SiteDTO> GetSiteById(int id)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Sites WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@id", id));
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                return new SiteDTO
                {
                    Id = reader.GetInt32("Id"),
                    Ville = reader.GetString("Ville")
                };
            }
            return new SiteDTO
            {
                Id = 0,
                Ville = string.Empty
            };
        }

        public async Task<bool> AddSite(SiteDTO site)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Sites (Ville) VALUES (@ville)";
            command.Parameters.Add(new MySqlParameter("@ville", site.Ville));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateSite(SiteDTO site)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Sites SET Ville = @ville WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@ville", site.Ville));
            command.Parameters.Add(new MySqlParameter("@id", site.Id));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteSite(SiteDTO site)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Sites WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@id", site.Id));
            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}