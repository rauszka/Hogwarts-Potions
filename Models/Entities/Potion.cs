using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Name { get; set; }
        public Student Student { get; set; }
        public HashSet<Ingredient> Ingredients { get; set; }
        public BrewingStatus BrewingStatus { get; set; } = BrewingStatus.Brew;
        public Recipe Recipe { get; set; }

        public Potion()
        {
            Ingredients = new HashSet<Ingredient>();
        }
    }
}
