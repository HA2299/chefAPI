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

        public Category AddItem(Category item)
        {
            _context.Categories.Add(item);
            _context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Categories.Remove(GetById(id));
            _context.Save();
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.ToList().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Category item)
        {
            var category = GetById(id);
            category.Id = item.Id;
            category.Name = item.Name;
            //category.Recipes = item.Recipes;
            _context.Save();
        }
    }
}
