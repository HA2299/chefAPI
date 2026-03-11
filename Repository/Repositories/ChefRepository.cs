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

        public Chef AddItem(Chef item)
        {
            _context.Chefs.Add(item);

            _context.Save();    
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Chefs.Remove(GetById(id));
            _context.Save();
        }

        public List<Chef> GetAll()
        {
            return (List<Chef>)_context.Chefs.Include(x=>x.User).ToList();
        }

        public Chef GetById(int id)
        {
            return _context.Chefs.Include(x=>x.User).ToList().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Chef item)
        {
            var Chef = GetById(id);
            Chef.Id = item.Id;
            Chef.UserId = item.UserId;
            Chef.User = item.User;
            Chef.Recipes = item.Recipes;
            Chef.AverageRating= item.AverageRating;
            _context.Save();
        }

    }
}
