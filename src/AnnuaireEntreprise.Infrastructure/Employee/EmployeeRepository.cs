// Author: Salar
// Created: 06/01/2024
using MySql.Data.MySqlClient;
using AnnuaireEntreprise.Model.Models;
using System.Data;
using System.Text;

namespace AnnuaireEntreprise.Infrastructure
{
    public partial class EmployeeRepository(MySqlConnector connector) : IEmployeeRepository
    {
        private readonly MySqlConnector _connector = connector;

        public async Task<List<EmployeeDTO>> GetAllEmployees(int page, int pageSize)
        {
            using var connection = _connector.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();

            // SQL query to join Employees with Sites and Services
            command.CommandText = @"
        SELECT e.Id, e.Nom, e.Prenom, e.Email, e.TelephoneFixe, e.TelephonePortable, 
               s.Id AS SiteId, s.Ville AS SiteName, 
               srv.Id AS ServiceId, srv.Nom AS ServiceName
        FROM Employees e
        LEFT JOIN Sites s ON e.SiteId = s.Id
        LEFT JOIN Services srv ON e.ServiceId = srv.Id
        ORDER BY e.Id
        LIMIT @PageSize OFFSET @Offset";

            command.Parameters.AddWithValue("@PageSize", pageSize);
            command.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);

            using var reader = await command.ExecuteReaderAsync();
            var employees = new List<EmployeeDTO>();
            while (await reader.ReadAsync())
            {
                employees.Add(new EmployeeDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nom = reader.GetString(reader.GetOrdinal("Nom")),
                    Prenom = reader.GetString(reader.GetOrdinal("Prenom")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    TelephoneFixe = reader.IsDBNull(reader.GetOrdinal("TelephoneFixe"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("TelephoneFixe")),
                    TelephonePortable = reader.GetString(reader.GetOrdinal("TelephonePortable")),
                    Site = new SiteDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("SiteId")),
                        Ville = reader.GetString(reader.GetOrdinal("SiteName"))
                    },
                    Service = new ServiceDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                        Nom = reader.GetString(reader.GetOrdinal("ServiceName"))
                    }
                });
            }

            return employees;
        }

        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            using var connection = _connector.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();

            // Query to include Site and Service joins
            command.CommandText = @"
        SELECT e.*, s.Id AS SiteId, s.Ville AS SiteName, 
               srv.Id AS ServiceId, srv.Nom AS ServiceName
        FROM Employees e
        LEFT JOIN Sites s ON e.SiteId = s.Id
        LEFT JOIN Services srv ON e.ServiceId = srv.Id
        WHERE e.Id = @id";

            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new EmployeeDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nom = reader.GetString(reader.GetOrdinal("Nom")),
                    Prenom = reader.GetString(reader.GetOrdinal("Prenom")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    TelephoneFixe = reader.IsDBNull(reader.GetOrdinal("TelephoneFixe"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("TelephoneFixe")),
                    TelephonePortable = reader.GetString(reader.GetOrdinal("TelephonePortable")),
                    Site = new SiteDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("SiteId")),
                        Ville = reader.GetString(reader.GetOrdinal("SiteName"))
                    },
                    Service = new ServiceDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                        Nom = reader.GetString(reader.GetOrdinal("ServiceName"))
                    }
                };
            }

            return null ?? new EmployeeDTO
            {
                Nom = string.Empty,
                Prenom = string.Empty,
                Email = string.Empty,
                TelephonePortable = string.Empty,
                Site = new SiteDTO
                {
                    Id = 0,
                    Ville = string.Empty
                },
                Service = new ServiceDTO
                {
                    Id = 0,
                    Nom = string.Empty
                }
            };
        }


        public async Task<CreateEmployeeDTO> AddEmployee(CreateEmployeeDTO employee)
        {
            using var connection = _connector.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"
        INSERT INTO Employees (Nom, Prenom, Email, TelephoneFixe, TelephonePortable, SiteId, ServiceId) 
        VALUES (@nom, @prenom, @email, @telephoneFixe, @telephonePortable, @siteId, @serviceId);
        SELECT LAST_INSERT_ID();";

            command.Parameters.AddWithValue("@nom", employee.Nom);
            command.Parameters.AddWithValue("@prenom", employee.Prenom);
            command.Parameters.AddWithValue("@email", employee.Email);
            command.Parameters.AddWithValue("@telephoneFixe", employee.TelephoneFixe ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@telephonePortable", employee.TelephonePortable);
            command.Parameters.AddWithValue("@siteId", employee.SiteId);
            command.Parameters.AddWithValue("@serviceId", employee.ServiceId);

            var newId = Convert.ToInt32(await command.ExecuteScalarAsync());
            employee.Id = newId;
            return employee;
        }


        public async Task<bool> UpdateEmployee(CreateEmployeeDTO employee)
        {
            using var connection = _connector.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"
            UPDATE Employees 
            SET Nom = @nom, Prenom = @prenom, Email = @email, 
                TelephoneFixe = @telephoneFixe, TelephonePortable = @telephonePortable, 
                SiteId = @siteId, ServiceId = @serviceId
            WHERE Id = @id";

            command.Parameters.AddWithValue("@nom", employee.Nom);
            command.Parameters.AddWithValue("@prenom", employee.Prenom);
            command.Parameters.AddWithValue("@email", employee.Email);
            command.Parameters.AddWithValue("@telephoneFixe", employee.TelephoneFixe ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@telephonePortable", employee.TelephonePortable);
            command.Parameters.AddWithValue("@siteId", employee.SiteId);
            command.Parameters.AddWithValue("@serviceId", employee.ServiceId);
            command.Parameters.AddWithValue("@id", employee.Id);

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


        public async Task<List<EmployeeDTO>> FetchEmployeesByArg(
            SearchEmployeeDTO searchEmployeeDTO)
        {
            using var connection = _connector.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();

            var query = new StringBuilder(@"
                SELECT e.*, s.Id AS SiteId, s.Ville AS SiteName, 
                    srv.Id AS ServiceId, srv.Nom AS ServiceName
                FROM Employees e
                LEFT JOIN Sites s ON e.SiteId = s.Id
                LEFT JOIN Services srv ON e.ServiceId = srv.Id
                WHERE 1=1 ");

            if (!string.IsNullOrWhiteSpace(searchEmployeeDTO.Nom))
            {
                query.Append("AND e.Nom LIKE @nom ");
                command.Parameters.AddWithValue("@nom", $"%{searchEmployeeDTO.Nom}%");
            }
            if (!string.IsNullOrWhiteSpace(searchEmployeeDTO.Prenom))
            {
                query.Append("AND e.Prenom LIKE @prenom ");
                command.Parameters.AddWithValue("@prenom", $"%{searchEmployeeDTO.Prenom}%");
            }
            if (!string.IsNullOrWhiteSpace(searchEmployeeDTO.Email))
            {
                query.Append("AND e.Email LIKE @email ");
                command.Parameters.AddWithValue("@email", $"%{searchEmployeeDTO.Email}%");
            }
            if (!string.IsNullOrWhiteSpace(searchEmployeeDTO.Telephone))
            {
                query.Append("AND (e.TelephoneFixe LIKE @telephone OR e.TelephonePortable LIKE @telephone) ");
                command.Parameters.AddWithValue("@telephone", $"%{searchEmployeeDTO.Telephone}%");
            }
            if (searchEmployeeDTO.SiteId.HasValue)
            {
                query.Append("AND e.SiteId = @siteId ");
                command.Parameters.AddWithValue("@siteId", searchEmployeeDTO.SiteId.Value);
            }
            if (searchEmployeeDTO.ServiceId.HasValue)
            {
                query.Append("AND e.ServiceId = @serviceId ");
                command.Parameters.AddWithValue("@serviceId", searchEmployeeDTO.ServiceId.Value);
            }


            query.Append("ORDER BY e.Id LIMIT @PageSize OFFSET @Offset");
            command.Parameters.AddWithValue("@PageSize", searchEmployeeDTO.PageSize);
            command.Parameters.AddWithValue("@Offset", (searchEmployeeDTO.Page - 1) * searchEmployeeDTO.PageSize);

            command.CommandText = query.ToString();

            using var reader = await command.ExecuteReaderAsync();
            var employees = new List<EmployeeDTO>();
            while (await reader.ReadAsync())
            {
                employees.Add(new EmployeeDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nom = reader.GetString(reader.GetOrdinal("Nom")),
                    Prenom = reader.GetString(reader.GetOrdinal("Prenom")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    TelephoneFixe = reader.IsDBNull(reader.GetOrdinal("TelephoneFixe"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("TelephoneFixe")),
                    TelephonePortable = reader.GetString(reader.GetOrdinal("TelephonePortable")),
                    Site = new SiteDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("SiteId")),
                        Ville = reader.GetString(reader.GetOrdinal("SiteName"))
                    },
                    Service = new ServiceDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                        Nom = reader.GetString(reader.GetOrdinal("ServiceName"))
                    }
                });
            }

            return employees;
        }

        public async Task<int> FetchTotalEmployeeCount()
        {
            using var connection = _connector.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Employees";
            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

    }
}