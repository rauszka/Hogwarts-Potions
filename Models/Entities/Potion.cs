using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public Student Student { get; set; }
        public HashSet<Ingredient> PotionIngredients { get; set; }

        public BrewingStatus Status { get; set; } = BrewingStatus.Brew;

        public Recipe Recipe { get; set; }

        public Potion()
        {
            PotionIngredients = new HashSet<Ingredient>();
        }
    }
}
    