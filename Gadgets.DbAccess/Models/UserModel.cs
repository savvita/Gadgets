namespace Gadgets.DbAccess.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
