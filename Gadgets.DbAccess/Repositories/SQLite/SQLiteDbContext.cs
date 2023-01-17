namespace Gadgets.DbAccess.Repositories.SQLite
{
    public class SQLiteDbContext : DbContext
    {
        public SQLiteDbContext(DbConfig configuration) : base(new GadgetRepository(configuration), new UserRepository(configuration))
        {

        }
    }
}
