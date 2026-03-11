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
    public class UserService(IRepository<User> repository) : IService<User>
    {
        private readonly IRepository<User> repository = repository;

        public User AddItem(User item)
        {

            return repository.AddItem(item);
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<User> GetAll()
        {
            return repository.GetAll();
        }

        public User GetById(int id)
        {
            return repository.GetById(id);
        }

        public void UpdateItem(int id, User item)
        {
            repository.UpdateItem(id, item);
        }
    }
}
