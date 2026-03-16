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
        Task<List<RecipeDto>> GetRecipesByChefIdAsync(int chefId);
        Task<ChefDto> GetByUserIdAsync(int userId);
        Task UpdateChefRatingAsync(int chefId, double newRating);


    }
}
