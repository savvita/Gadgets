using Gadgets.DbAccess.Models;

namespace Gadgets.DbAccess.Repositories
{
    public interface IGadgetRepository : IRepository<GadgetModel>
    {
        Task<IEnumerable<GadgetModel>> GetAsync(string model);
    }
}
