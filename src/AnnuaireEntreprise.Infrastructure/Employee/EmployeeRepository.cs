// Author: Salar
// Created: 06/01/2024
using MySql.Data.MySqlClient;
using AnnuaireEntreprise.Model.Models;
using System.Data;

namespace AnnuaireEntreprise.Infrastructure
{
    public partial class EmployeeRepository : IEmployeeRepository
    {
        private readonly MySqlConnector _connector;

        public EmployeeRepository(MySqlConnector connector)
        {
            _connector = connector;
        }

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
                    Site = reader.GetString("Site"),
                    Service = reader.GetString("Service")
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
                    Site = reader.GetString("Site"),
                    Service = reader.GetString("Service")
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
                Site = string.Empty,
                Service = string.Empty
            };
        }

        public async Task<bool> AddEmployee(EmployeeDTO employee)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Employees (Nom, Prenom, Email, TelephoneFixe, TelephonePortable, Site, Service) VALUES (@nom, @prenom, @email, @telephoneFixe, @telephonePortable, @site, @service)";
            command.Parameters.Add(new MySqlParameter("@nom", employee.Nom));
            command.Parameters.Add(new MySqlParameter("@prenom", employee.Prenom));
            command.Parameters.Add(new MySqlParameter("@email", employee.Email));
            command.Parameters.Add(new MySqlParameter("@telephoneFixe", employee.TelephoneFixe));
            command.Parameters.Add(new MySqlParameter("@telephonePortable", employee.TelephonePortable));
            command.Parameters.Add(new MySqlParameter("@site", employee.Site));
            command.Parameters.Add(new MySqlParameter("@service", employee.Service));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateEmployee(EmployeeDTO employee)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Employees SET Nom = @nom, Prenom = @prenom, Email = @email, TelephoneFixe = @telephoneFixe, TelephonePortable = @telephonePortable, Site = @site, Service = @service WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@nom", employee.Nom));
            command.Parameters.Add(new MySqlParameter("@prenom", employee.Prenom));
            command.Parameters.Add(new MySqlParameter("@email", employee.Email));
            command.Parameters.Add(new MySqlParameter("@telephoneFixe", employee.TelephoneFixe));
            command.Parameters.Add(new MySqlParameter("@telephonePortable", employee.TelephonePortable));
            command.Parameters.Add(new MySqlParameter("@site", employee.Site));
            command.Parameters.Add(new MySqlParameter("@service", employee.Service));
            command.Parameters.Add(new MySqlParameter("@id", employee.Id));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteEmployee(EmployeeDTO employee)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Employees WHERE Id = @id";
            command.Parameters.Add(new MySqlParameter("@id", employee.Id));
            return await command.ExecuteNonQueryAsync() > 0;
        }


        public async Task<List<EmployeeDTO>> SearchEmployeeByArg(string? nom, string? prenom, string? email, string? telephone, string? site, string? service)
        {
            using var connection = _connector.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            // Create  the sql command for arg those exist 
            command.CommandText = "SELECT * FROM Employees WHERE Nom = @nom OR Prenom = @prenom OR Email = @email OR TelephoneFixe = @telephone OR Site = @site OR Service = @service";
            command.Parameters.Add(new MySqlParameter("@nom", nom));
            command.Parameters.Add(new MySqlParameter("@prenom", prenom));
            command.Parameters.Add(new MySqlParameter("@email", email));
            command.Parameters.Add(new MySqlParameter("@telephone", telephone));
            command.Parameters.Add(new MySqlParameter("@site", site));
            command.Parameters.Add(new MySqlParameter("@service", service));
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
                    Site = reader.GetString("Site"),
                    Service = reader.GetString("Service")
                });
            }
            return employees ?? new List<EmployeeDTO>();
        }
    }
}