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

        public Favorite AddItem(Favorite item)
        {
            _context.Favorites.Add(item);

            _context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Favorites.Remove(GetById(id));
            _context.Save();
        }

        public List<Favorite> GetAll()
        {
            return _context.Favorites.ToList();
        }

        public Favorite GetById(int id)
        {
            return _context.Favorites.ToList().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Favorite item)
        {
            var Favorite = GetById(id);
            Favorite.Id = item.Id;
            Favorite.UserId = item.UserId;
            Favorite.User = item.User;
            Favorite.RecipeId = item.RecipeId;
            Favorite.Recipe= item.Recipe;
            _context.Save();
        }
    }
}
