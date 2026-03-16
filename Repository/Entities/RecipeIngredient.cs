using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Repository.Entities
{
    public enum UnitType
    {
        Grams,
        Kilograms,
        Milliliters,
        Liters,
        Cups,
        Tablespoons,
        Teaspoons,
        Pieces
    }
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [ForeignKey("Recipe")]
        public required int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        [ForeignKey("Ingredient")]
        public required int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        public decimal Quantity { get; set; }
        public UnitType Unit { get; set; }
    }
}
