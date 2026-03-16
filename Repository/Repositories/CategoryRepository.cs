using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepository(IContext context) : IRepository<Category>
    {

        private readonly IContext _context = context;

        public async Task<Category> AddItemAsync(Category item)
        {
            await _context.Categories.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task DeleteItemAsync(int id)
        {
            _context.Categories.Remove(await GetByIdAsync(id));
             await _context.Save();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.SingleAsync(x=>x.Id == id);
        }

        public async Task UpdateItemAsync(int id, Category item)
        {
            var category = await GetByIdAsync(id);
            category.Id = item.Id;
            category.Name = item.Name;
            //category.Recipes = item.Recipes;
            await _context.Save();
        }
    }
}
