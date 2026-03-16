using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IRecipe
    {
        Task AddRatingAsync(int recipeId, int ratingValue);
    }
}
