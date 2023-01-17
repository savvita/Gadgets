namespace Gadgets.DbAccess.Repositories
{
    public class DbContext
    {
        public IGadgetRepository Gadgets { get; }
        public IUserRepository Users { get; }
        public DbContext(IGadgetRepository gadgets, IUserRepository users)
        {
            Gadgets = gadgets;
            Users = users;
        }
    }
}
