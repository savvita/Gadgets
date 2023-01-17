using Gadgets.API.Data;
using Gadgets.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Gadgets.API.Controllers
{
    [ApiController]
    [Route("api/gadgets")]
    [Authorize]
    public class GadgetController
    {
        private GadgetsDbContext context;
        public GadgetController(GadgetsDbContext context)
        {
            this.context = context;
        }

        [HttpGet("")]
        public async Task<List<Gadget>> Get()
        {
            return await context.Gadgets.GetAllAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<Gadget?> Get(int id)
        {
            return await context.Gadgets.GetAsync(id);
        }

        [HttpGet("model")]
        public async Task<List<Gadget>> Get(string model)
        {
            return await context.Gadgets.GetAsync(model);
        }

        [HttpPost("")]
        public async Task<Gadget> Create([FromBody]Gadget gadget)
        {
            return await context.Gadgets.CreateAsync(gadget);
        }

        [HttpPut("")]
        public async Task<Gadget> Update([FromBody]Gadget gadget)
        {
            return await context.Gadgets.UpdateAsync(gadget);
        }

        [HttpDelete("{id:int}")]
        public async Task Remove(int id)
        {
            await context.Gadgets.DeleteAsync(id);
        }
    }
}
