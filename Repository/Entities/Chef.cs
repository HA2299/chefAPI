using Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class Chef
{
    public int Id { get; set; }

    [ForeignKey("User")]
    public int? UserId { get; set; } // שינוי ל-int?
    public virtual User? User { get; set; }
    public string? ImageUrl { get; set; } //התמונה כמחרוזת
    public double AverageRating { get; set; }
    public ICollection<Recipe> Recipes { get; set; }
}