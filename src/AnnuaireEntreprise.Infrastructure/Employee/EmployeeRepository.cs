// Author: Salar
// Created: 06/01/2024
using MySql.Data.MySqlClient;
using AnnuaireEntreprise.Model.Models;
using System.Data;

namespace AnnuaireEntreprise.Infrastructure
{
    public partial class EmployeeRepository(MySqlConnector connector) : IEmployeeRepository
    {
        private readonly MySqlConnector _connector = connector;

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Employees";
            using var reader = await command.ExecuteReaderAsync();
            var employees = new List<EmployeeDTO>();
            while (await reader.ReadAsync())
            {
                employees.Add(new EmployeeDTO
                {
                    Id = reader.GetInt32("Id"),
                    Nom = reader.GetString("Nom"),
                    Prenom = reader.GetString("Prenom"),
                    Email = reader.GetString("Email"),
                    TelephoneFixe = reader.GetString("TelephoneFixe"),
                    TelephonePortable = reader.GetString("TelephonePortable"),
                    SiteId = reader.GetInt32("SiteId"),
                    ServiceId = reader.GetInt32("ServiceId")
                });
            }
            return employees ?? new List<EmployeeDTO>();
        }

        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Employees WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@id", id));
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                return new EmployeeDTO
                {
                    Id = reader.GetInt32("Id"),
                    Nom = reader.GetString("Nom"),
                    Prenom = reader.GetString("Prenom"),
                    Email = reader.GetString("Email"),
                    TelephoneFixe = reader.GetString("TelephoneFixe"),
                    TelephonePortable = reader.GetString("TelephonePortable"),
                    SiteId = reader.GetInt32("SiteId"),
                    ServiceId = reader.GetInt32("ServiceId")
                };
            }
            return new EmployeeDTO
            {
                Id = 0,
                Nom = string.Empty,
                Prenom = string.Empty,
                Email = string.Empty,
                TelephoneFixe = string.Empty,
                TelephonePortable = string.Empty,
                SiteId = 0,
                ServiceId = 0
            };
        }

        public async Task<EmployeeDTO> AddEmployee(EmployeeDTO employee)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Employees (Nom, Prenom, Email, TelephoneFixe, TelephonePortable, SiteId, ServiceId) VALUES (@nom, @prenom, @email, @telephoneFixe, @telephonePortable, @site, @service)";
            command.CommandText += "; SELECT LAST_INSERT_ID();";
            command.Parameters.Add(new MySqlParameter("@nom", employee.Nom));
            command.Parameters.Add(new MySqlParameter("@prenom", employee.Prenom));
            command.Parameters.Add(new MySqlParameter("@email", employee.Email));
            command.Parameters.Add(new MySqlParameter("@telephoneFixe", employee.TelephoneFixe));
            command.Parameters.Add(new MySqlParameter("@telephonePortable", employee.TelephonePortable));
            command.Parameters.Add(new MySqlParameter("@site", employee.SiteId));
            command.Parameters.Add(new MySqlParameter("@service", employee.ServiceId));
            long Id = await command.ExecuteNonQueryAsync();
            Id = Id > 0 ? (int)command.LastInsertedId : 0;
            employee.Id = (int)Id;
            return employee;
        }

        public async Task<bool> UpdateEmployee(EmployeeDTO employee)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Employees SET Nom = @nom, Prenom = @prenom, Email = @email, TelephoneFixe = @telephoneFixe, TelephonePortable = @telephonePortable, SiteId = @site, ServiceId = @service WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@nom", employee.Nom));
            command.Parameters.Add(new MySqlParameter("@prenom", employee.Prenom));
            command.Parameters.Add(new MySqlParameter("@email", employee.Email));
            command.Parameters.Add(new MySqlParameter("@telephoneFixe", employee.TelephoneFixe));
            command.Parameters.Add(new MySqlParameter("@telephonePortable", employee.TelephonePortable));
            command.Parameters.Add(new MySqlParameter("@site", employee.SiteId));
            command.Parameters.Add(new MySqlParameter("@service", employee.ServiceId));
            command.Parameters.Add(new MySqlParameter("@id", employee.Id));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Employees WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@id", id));
            return await command.ExecuteNonQueryAsync() > 0;
        }


        public async Task<List<EmployeeDTO>> SearchEmployeeByArg(string? nom, string? prenom, string? email, string? telephone, int? siteId, int? serviceId)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            // Create  the sql command for arg those exist 
            command.CommandText = "SELECT * FROM Employees WHERE Nom = @nom OR Prenom = @prenom OR Email = @email OR TelephoneFixe = @telephone OR SiteId = @site OR ServiceId = @service";
            command.Parameters.Add(new MySqlParameter("@nom", nom));
            command.Parameters.Add(new MySqlParameter("@prenom", prenom));
            command.Parameters.Add(new MySqlParameter("@email", email));
            command.Parameters.Add(new MySqlParameter("@telephone", telephone));
            command.Parameters.Add(new MySqlParameter("@site", siteId));
            command.Parameters.Add(new MySqlParameter("@service", serviceId));
            using var reader = await command.ExecuteReaderAsync();
            var employees = new List<EmployeeDTO>();
            while (await reader.ReadAsync())
            {
                employees.Add(new EmployeeDTO
                {
                    Id = reader.GetInt32("Id"),
                    Nom = reader.GetString("Nom"),
                    Prenom = reader.GetString("Prenom"),
                    Email = reader.GetString("Email"),
                    TelephoneFixe = reader.GetString("TelephoneFixe"),
                    TelephonePortable = reader.GetString("TelephonePortable"),
                    SiteId = reader.GetInt32("SiteId"),
                    ServiceId = reader.GetInt32("ServiceId")
                });
            }
            return employees ?? new List<EmployeeDTO>();
        }
    }
}