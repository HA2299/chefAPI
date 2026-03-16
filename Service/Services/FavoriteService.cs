using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interfaces;
using Repository.Entities;
using Repository.interfaces;

namespace Service.Services
{
    public class FavoriteService : IService<Favorite>
    {
        private readonly IRepository<Favorite> repository;
        public FavoriteService(IRepository<Favorite> repository)
        {
            this.repository = repository;
        }
        public async Task<Favorite> AddItemAsync(Favorite item)
        {
            return await repository.AddItemAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await repository.DeleteItemAsync(id);
        }

        public async Task<List<Favorite>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Favorite> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task UpdateItemAsync(int id, Favorite item)
        {
            await repository.UpdateItemAsync(id, item);
       }
   }
}
