using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities
{
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; }
        public Student CreatorStudent { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }

    }
}
