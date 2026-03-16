using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ChefRepository(IContext context) : IRepository<Chef>
    {

        private readonly IContext _context = context;

        public async Task<Chef> AddItemAsync(Chef item)
        {
            await _context.Chefs.AddAsync(item);
            await _context.Save();    
            return item;
        }

        public async Task DeleteItemAsync(int id)
        {
            _context.Chefs.Remove(await GetByIdAsync(id));
            await _context.Save();
        }

        public async Task<List<Chef>> GetAllAsync()
        {
            return await _context.Chefs.Include(x => x.User).ToListAsync();
        }

        public async Task<Chef> GetByIdAsync(int id)
        {
            return await _context.Chefs.Include(x=>x.User).SingleAsync(x=>x.Id == id);
        }

        public async Task UpdateItemAsync(int id, Chef item)
        {
            var Chef =  await GetByIdAsync(id);
            Chef.Id = item.Id;
            Chef.UserId = item.UserId;
            Chef.User = item.User;
            Chef.Recipes = item.Recipes;
            Chef.AverageRating= item.AverageRating;
            await _context.Save();
        }

    }
}
