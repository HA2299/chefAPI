using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Repository.Entities;

namespace Service.Dto
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }
        public int PreparationTime { get; set; } // in minutes
        public int CookingTime { get; set; } // in minutes
        public int NumDoses { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }

        [ForeignKey("Chef")]
        public int ChefId { get; set; }
        public string? ChefName { get; set; } // שם השף

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public byte[]? Image { get; set; } //התמונה כמחרוזת
        public IFormFile? FileImage { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }

    }
}
