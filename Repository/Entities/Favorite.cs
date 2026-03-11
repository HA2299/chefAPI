using Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class Favorite
{
    public int Id { get; set; }

    [ForeignKey("User")]
    public int? UserId { get; set; } // שינוי ל-int?
    public User User { get; set; }

    [ForeignKey("Recipe")]
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}