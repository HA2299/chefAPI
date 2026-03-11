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
    public class CategoryService(IRepository<Category> repository) : IService<Category>
    {
        private readonly IRepository<Category> repository = repository;

        public Category AddItem(Category item)
        {

            return repository.AddItem(item);
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<Category> GetAll()
        {
            return repository.GetAll();
        }

        public Category GetById(int id)
        {
           return repository.GetById(id);
        }

        public void UpdateItem(int id, Category item)
        {
            repository.UpdateItem(id, item);
        }
    }
}
