using Gadgets.DbAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace Gadgets.API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
        public User() {  }

        public User(UserModel model)
        {
            Id= model.Id;
            Login = model.Login;
            Password = model.Password;
        }
    }
}
