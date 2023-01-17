namespace Gadgets.DbAccess.Models
{
    public class GadgetModel
    {
        public int Id { get; set; }
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
