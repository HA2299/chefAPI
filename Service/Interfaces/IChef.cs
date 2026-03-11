using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Service.Dto;

namespace Service.Interfaces
{
    public interface IChef
    {
        List<RecipeDto> GetRecipesByChefId(int chefId);
        ChefDto GetByUserId(int userId);
        void UpdateChefRating(int chefId, double newRating);


    }
}
