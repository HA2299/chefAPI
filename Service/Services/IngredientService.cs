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
    public class IngredientService(IRepository<Ingredient> repository) : IService<Ingredient>
    {
        private readonly IRepository<Ingredient> repository = repository;

        public Ingredient AddItem(Ingredient item)
        {

            return repository.AddItem(item);
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<Ingredient> GetAll()
        {
            return repository.GetAll();
        }

        public Ingredient GetById(int id)
        {
            return repository.GetById(id);
        }
        
        public Ingredient GetByName(string name)
        {
            return repository.GetByName(name);
        }

        public void UpdateItem(int id, Ingredient item)
        {
            repository.UpdateItem(id, item);
        }
    }
}
