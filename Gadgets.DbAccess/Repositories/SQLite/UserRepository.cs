using Dapper;
using Gadgets.DbAccess.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Gadgets.DbAccess.Repositories.SQLite
{
    public class UserRepository : IUserRepository
    {
        private DbConfig configuration;
        public UserRepository(DbConfig configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> Check(UserModel user)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            try
            {
                await connection.QueryFirstAsync<int>("select Id from Users where Login = @Login and Password = @Password limit 1;", user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserModel> CreateAsync(UserModel entity)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            int rows = await connection.ExecuteAsync("insert into Users (Login, Password) values (@Login, @Password);", entity);

            if (rows != 0)
            {
                entity.Id = await connection.QueryFirstOrDefaultAsync<int>("select Id from Users order by Id desc limit 1;");
            }

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            await connection.ExecuteAsync("delete from Users where Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            return await connection.QueryAsync<UserModel>("select * from Users;");
        }

        public async Task<UserModel?> GetAsync(int id)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<UserModel>("select * from Users where Id = @Id;", new { Id = id });
        }

        public async Task<UserModel> UpdateAsync(UserModel entity)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            int rows = await connection.ExecuteAsync("update Users set Login = @Login, Password = @Password where Id = @Id", entity);

            if (rows == 0)
            {
                throw new Exception("User not found");
            }

            return entity;
        }
    }
}
