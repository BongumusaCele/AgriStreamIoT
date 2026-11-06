using AgriStreamIoT.Models;
using Microsoft.AspNetCore.Components.Sections;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AgriStreamIoT.Repository
{
    public sealed class AgriStreamRepository
    {
        private const string Columns = "UserId, Username, Password, Role";
        private readonly IConfiguration _configuration;
        public async Task<SaveResult> SaveAsync(Users user, CancellationToken ct = default)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync(ct);
            await using var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO dbo.AgriStreamDb ({Columns}) VALUES (@id,@username,@password,@role)";
            command.Parameters.Add("@id", SqlDbType.NVarChar, 50).Value = user.UserId;
            command.Parameters.Add("@username", SqlDbType.NVarChar, 20).Value = user.Username;
            command.Parameters.Add("@role", SqlDbType.NVarChar, 20).Value = user.Role;
            command.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = user.Password;
            
            try
            {
                await command.ExecuteNonQueryAsync(ct);
                return new SaveResult(user, true);
            }
            catch (SqlException ex) when (ex.Number is 2601 or 2627)
            {
                Console.WriteLine("Error while trying to store user: " + ex);
                throw new Exception();
            }
        }
    }
}
