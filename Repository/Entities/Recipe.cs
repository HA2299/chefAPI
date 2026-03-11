using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Repository.Entities
{
    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Difficult
    }
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }
        public int PreparationTime { get; set; } // in minutes
        public int CookingTime { get; set; } // in minutes
        public int NumDoses { get; set; }
        public string? ImageUrl { get; set; } //התמונה כמחרוזת

        public DifficultyLevel DifficultyLevel { get; set; }

        [ForeignKey("Chef")]
        public int ChefId { get; set; }
        public virtual Chef? Chef { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
    }
}
