using Gadgets.API.Models;
using Gadgets.DbAccess.Repositories;

namespace Gadgets.API.Data
{
    public class GadgetRepository
    {
        private DbContext context;
        public GadgetRepository(DbContext context)
        {
            this.context = context;
        }
        public async Task<List<Gadget>> GetAllAsync()
        {
            return (await context.Gadgets.GetAllAsync()).Select(model => new Gadget(model)).ToList();
        }

        public async Task<Gadget?> GetAsync(int id)
        {
            var model = await context.Gadgets.GetAsync(id);

            return model != null ? new Gadget(model) : null;
        }

        public async Task<List<Gadget>> GetAsync(string model)
        {
            var models = await context.Gadgets.GetAsync(model);

            return models.Select(m => new Gadget(m)).ToList();
        }


        public async Task<Gadget> UpdateAsync(Gadget gadget)
        {
            return new Gadget(await context.Gadgets.UpdateAsync(new DbAccess.Models.GadgetModel()
            {
                Id = gadget.Id,
                Model = gadget.Model,
                Price = gadget.Price
            }));
        }
        public async Task DeleteAsync(int id)
        {
            await context.Gadgets.DeleteAsync(id);
        }

        public async Task<Gadget> CreateAsync(Gadget gadget)
        {
            return new Gadget(await context.Gadgets.CreateAsync(new DbAccess.Models.GadgetModel()
            {
                Model = gadget.Model,
                Price = gadget.Price
            }));
        }
    }
}
