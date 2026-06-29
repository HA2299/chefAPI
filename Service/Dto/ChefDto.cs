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
    public class ChefDto
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public double AverageRating {  get; set; }
        public byte[]? Image { get; set; } //התמונה כמחרוזת
        public IFormFile? FileImage { get; set; }
    }
}
