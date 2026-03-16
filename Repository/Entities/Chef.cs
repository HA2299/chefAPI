using Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class Chef
{
    public int Id { get; set; }

    [ForeignKey("User")]
    public required int? UserId { get; set; }
    public virtual User? User { get; set; }
    public string? ImageUrl { get; set; }
    public double AverageRating { get; set; }
    public ICollection<Recipe> Recipes { get; set; }
}