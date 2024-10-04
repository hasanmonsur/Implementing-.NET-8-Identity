using Dapper;
using Npgsql;
using System.Data.Common;
using System.Data;
using WebIdentityApp.Contacts;
using WebIdentityApp.Data;
using WebIdentityApp.Models;

namespace WebIdentityApp.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;

        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using var connection = _dapperContext.CreateDbConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE Email = @Email", new { Email = email });
        }

        public async Task<User> CreateUserAsync(User user)
        {
            using var connection = _dapperContext.CreateDbConnection();
            if (connection.State == ConnectionState.Open)
            {
                user.Id = Guid.NewGuid().ToString();
                var sql = "INSERT INTO users (Id, Email, PasswordHash) VALUES (@Id, @Email, @PasswordHash)";
                await connection.ExecuteAsync(sql, user);
                return user;
            }
            else
            {
                return null;
            }
        
        }

        public async Task<bool> ConfirmEmailAsync(string userId)
        {
            using var connection = _dapperContext.CreateDbConnection();
            var sql = "UPDATE users SET EmailConfirmed = TRUE WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = userId });
            return rowsAffected > 0;
        }
    }
}
