using Gadgets.DbAccess.Repositories;

namespace Gadgets.API.Data
{
    public class GadgetsDbContext
    {
        public GadgetRepository Gadgets { get; }
        public UserRepository Users { get; }
        public GadgetsDbContext(DbContext context)
        {
            Gadgets = new GadgetRepository(context);
            Users = new UserRepository(context);
        }

    }
}
