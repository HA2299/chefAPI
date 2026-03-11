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
        public Favorite AddItem(Favorite item)
        {

            return repository.AddItem(item);
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<Favorite> GetAll()
        {
            return repository.GetAll();
        }

        public Favorite GetById(int id)
        {
            return repository.GetById(id);
        }

        public void UpdateItem(int id, Favorite item)
        {
            repository.UpdateItem(id, item);
       }
   }
}
