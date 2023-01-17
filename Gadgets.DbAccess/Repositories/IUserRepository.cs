using Gadgets.DbAccess.Models;

namespace Gadgets.DbAccess.Repositories
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<bool> Check(UserModel user);
    }
}
