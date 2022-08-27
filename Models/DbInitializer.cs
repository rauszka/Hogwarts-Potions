using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models
{
    public static class DbInitializer
    {
        public static void Initialize(HogwartsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students,rooms,potions,ingredients.
            if (context.Students.Any() || context.Rooms.Any() || context.Potions.Any() || context.Ingredients.Any()) 

            {
                return;  // Database has been seeded
            }

            Student draco = new Student { Name = "Draco", HouseType = HouseType.Slytherin, PetType = PetType.Owl };
            Student harry = new Student { Name = "Harry", HouseType = HouseType.Gryffindor, PetType = PetType.Owl };
            Student ron = new Student { Name = "Ron", HouseType = HouseType.Gryffindor, PetType = PetType.Rat };

            Ingredient danRoot = new Ingredient { Name = "Dandelion root" };
            Ingredient dragBlood = new Ingredient { Name = "Dragon blood" };
            Ingredient firefly = new Ingredient { Name = "Firefly" };
            Ingredient frog = new Ingredient { Name = "Frog" };
            Ingredient iguanaBlood = new Ingredient { Name = "Iguana blood" };

            Recipe Amortentia = new Recipe
            {
                Name = "Amortentia",
                Ingredients = new HashSet<Ingredient> { danRoot, frog, iguanaBlood },
                Student = harry
            };

            Recipe Veritaserum = new Recipe
            {
                Name = "Veritaserum",
                Ingredients = new HashSet<Ingredient> { firefly },
                Student = draco
            };

            Potion potion1 = new Potion()
            {
                BrewingStatus = BrewingStatus.Brew,
                Name = "Potion",
                Student = harry,
                Ingredients = { danRoot, frog, iguanaBlood },
                Recipe = Amortentia
            };

            Potion potion2 = new Potion()
            {
                BrewingStatus = BrewingStatus.Brew,
                Name = "Potion two",
                Student = draco,
                Ingredients = { firefly },
                Recipe = Veritaserum
            };

            Room[] rooms = 
            {
                new Room { Capacity = 5, Residents = new HashSet<Student> { harry }},
                new Room { Capacity = 5, Residents = new HashSet<Student> { draco }},
                new Room { Capacity = 5, Residents = new HashSet<Student> { ron }}
            };

            foreach (Room room in rooms)
            {
                context.Rooms.Add(room);
            }

            Recipe[] recipes = { Amortentia, Veritaserum };

            foreach (Recipe r in recipes)
            {
                context.Recipes.Add(r);
            }

            Potion[] potions = { potion1, potion2 };

            foreach (Potion potion in potions)
            {
                context.Potions.Add(potion);
            }

            context.SaveChanges();
        }
    }
}
