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
    public class FavoriteRepository(IContext context) : IRepository<Favorite>
    {

        private readonly IContext _context = context;

        public async Task<Favorite> AddItemAsync(Favorite item)
        {
            await _context.Favorites.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task DeleteItemAsync(int id)
        {
            _context.Favorites.Remove(await GetByIdAsync(id));
            await _context.Save();
        }

        public async Task<List<Favorite>> GetAllAsync()
        {
            return await _context.Favorites.ToListAsync();
        }

        public async Task<Favorite> GetByIdAsync(int id)
        {
            return await _context.Favorites.SingleAsync(x => x.Id == id);
        }

        public async Task UpdateItemAsync(int id, Favorite item)
        {
            var Favorite = await GetByIdAsync(id);
            Favorite.Id = item.Id;
            Favorite.UserId = item.UserId;
            Favorite.User = item.User;
            Favorite.RecipeId = item.RecipeId;
            Favorite.Recipe= item.Recipe;
            await _context.Save();
        }
    }
}
