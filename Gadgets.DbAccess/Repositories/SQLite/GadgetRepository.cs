using Dapper;
using Gadgets.DbAccess.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Gadgets.DbAccess.Repositories.SQLite
{
    public class GadgetRepository : IGadgetRepository
    {
        private DbConfig configuration;
        public GadgetRepository(DbConfig configuration)
        {
            this.configuration = configuration;
        }

        public async Task<GadgetModel> CreateAsync(GadgetModel entity)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            int rows = await connection.ExecuteAsync("insert into Gadgets (Model, Price) values (@Model, @Price);", entity);

            if (rows != 0)
            {
                entity.Id = await connection.QueryFirstOrDefaultAsync<int>("select Id from Gadgets order by Id desc limit 1;");
            }

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            await connection.ExecuteAsync("delete from Gadgets where Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<GadgetModel>> GetAsync(string model)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            return await connection.QueryAsync<GadgetModel>("select * from Gadgets where [Model] like @Model;",
                new { Model = $"%{model}%" });
        }

        public async Task<IEnumerable<GadgetModel>> GetAllAsync()
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            return await connection.QueryAsync<GadgetModel>("select * from Gadgets;");
        }

        public async Task<GadgetModel?> GetAsync(int id)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<GadgetModel>("select * from Gadgets where Id = @Id;", new { Id = id });
        }

        public async Task<GadgetModel> UpdateAsync(GadgetModel entity)
        {
            using IDbConnection connection = new SqliteConnection(configuration.ConnectionString);
            int rows = await connection.ExecuteAsync("update Gadgets set Model = @Model, Price = @Price where Id = @Id", entity);

            if(rows == 0)
            {
                throw new Exception("Gadget not found");
            }

            return entity;
        }
    }
}
