using Gadgets.DbAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace Gadgets.API.Models
{
    public class Gadget
    {
        public int Id { get; set; }

        [Required]
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
        public Gadget() { }
        public Gadget(GadgetModel model)
        {
            Id = model.Id;
            Model = model.Model;
            Price = model.Price;
        }
        
    }
}
