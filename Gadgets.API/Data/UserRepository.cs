using Gadgets.API.Models;
using Gadgets.DbAccess.Repositories;

namespace Gadgets.API.Data
{
    public class UserRepository
    {
        private DbContext context;
        public UserRepository(DbContext context)
        {
            this.context = context;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return (await context.Users.GetAllAsync()).Select(model => new User(model)).ToList();
        }

        public async Task<User?> GetAsync(int id)
        {
            var model = await context.Users.GetAsync(id);

            return model != null ? new User(model) : null;
        }


        public async Task<User> UpdateAsync(User user)
        {
            return new User(await context.Users.UpdateAsync(new DbAccess.Models.UserModel()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password
            }));
        }
        public async Task DeleteAsync(int id)
        {
            await context.Users.DeleteAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            return new User(await context.Users.CreateAsync(new DbAccess.Models.UserModel()
            {
                Login = user.Login,
                Password = user.Password
            }));
        }

        public async Task<bool> Check(User user)
        {
            return await context.Users.Check(new DbAccess.Models.UserModel()
            {
                Login = user.Login,
                Password = user.Password
            });
        }
    }
}
