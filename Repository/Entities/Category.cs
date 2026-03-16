using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Repository.Entities
{
    public enum CategoryType
    {
        Appetizer,
        MainCourse,
        Dessert,
        Beverage,
        Salad,
        Soup
    }
    public class Category
    {
        public int Id { get; set; }
        public required CategoryType Name { get; set; }
        //public ICollection<Recipe>? Recipes { get; set; }
    }
}
