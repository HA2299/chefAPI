using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Documents;
using Repository.Entities;

namespace AI.Interfaces
{
    public interface IRecipeDocumentBuilder
    {
        Task<RecipeDocument> BuildAsync(Recipe recipe);
    }
}
