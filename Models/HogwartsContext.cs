using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models
{
    public class HogwartsContext : DbContext
    {
        public const int MaxIngredientsForPotions = 5;
        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Potion> Potions { get; set; }
        private Random _random = new Random();

        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Room>().ToTable("Room");
            modelBuilder.Entity<Recipe>().ToTable("Recipe");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<Potion>().ToTable("Potion");
        }
        
        public async Task<List<Potion>> GetAllPotions()
        {
            Task<List<Potion>> potions = Potions.ToListAsync();
            return await potions;
        }

        public async Task<Potion> AddPotion(Potion potion)
        {

            var newPotion = new Potion()
            {
                Name = potion.Name,
                Student = potion.Student,
                Ingredients = potion.Ingredients,
                Recipe = potion.Recipe

            };

            if (newPotion.Ingredients.Count < MaxIngredientsForPotions)
            {
                newPotion.BrewingStatus = BrewingStatus.Brew;

                foreach (Ingredient potionIngredient in newPotion.Ingredients)
                {
                    if (!Ingredients.Any(i => i.Name == potionIngredient.Name))
                    {
                        await Ingredients.AddAsync(potionIngredient);
                    }
                }
            }
            else
            {

                if (Recipes.Any(recept => recept.Ingredients.All(newPotion.Ingredients.Contains) && recept.Ingredients.Count == newPotion.Ingredients.Count))
                {
                    newPotion.BrewingStatus = BrewingStatus.Replica;
                }
                else
                {
                    int counter = GetStudentPotions(newPotion.Student.ID).Result.Count;

                    var newRecipe = new Recipe()
                    {
                        Student = newPotion.Student, 
                        Name = $"{newPotion.Student.Name}'s discovery #{counter++}",
                        Ingredients = newPotion.Ingredients,
                    };

                    Recipes.Add(newRecipe);
                    newPotion.Recipe = newRecipe; 

                    newPotion.BrewingStatus = BrewingStatus.Discovery;

                    foreach (Ingredient potionIngredient in newPotion.Ingredients)
                    {
                        if (!Ingredients.Any(i => i.Name == potionIngredient.Name))
                        {
                            await Ingredients.AddAsync(potionIngredient);
                        }
                    }
                }
            }

            await Potions.AddAsync(newPotion);
            await SaveChangesAsync();
            return newPotion;
        }

        public async Task<List<Potion>> GetStudentPotions(long studentId)
        {
            return await Potions
                .Where(potion => potion.Student.ID == studentId)
                .ToListAsync();
        }

        public async Task<Potion> GetPotion(long potionId)
        {
            Task<Potion> potion = Potions.FindAsync(potionId).AsTask();
            return await potion;
        }

        public async Task<Ingredient> GetIngredient(string ingredientName)
        {
            Task<Ingredient> ingredient = Ingredients.FirstAsync(ingredient => ingredient.Name == ingredientName);
            return await ingredient;
        }

        public async Task<Ingredient> AddNewIngredient(string name)
        {
            Ingredient ingredient = new Ingredient() { Name = name };
            await Ingredients.AddAsync(ingredient);
            return ingredient;
        }

        public async Task<Potion> AddIngredient(long id, Ingredient newIngredient)
        {
            Ingredient ingredient;
            try
            {
                ingredient = await GetIngredient(newIngredient.Name);
            }
            catch
            {
                ingredient = await AddNewIngredient(newIngredient.Name);
            }

            Potion potion = await GetPotion(id);
            potion.Ingredients.Add(ingredient);
            await SaveChangesAsync();
            return potion;
        }

        public async Task<List<Recipe>> GetHelp(long id)
        {
            Potion potion = await GetPotion(id);

            return Recipes
                .AsEnumerable()
                .Where(r => r.Ingredients.SequenceEqual(potion.Ingredients)).ToList();
        }
    }
}
