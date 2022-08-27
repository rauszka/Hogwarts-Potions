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

        public async Task AddRoom(Room room)
        {
            await Rooms.AddAsync(room);
        }

        public async Task<Room> GetRoom(long roomId)
        {
            Task<Room> room = Rooms.FindAsync(roomId).AsTask();
            return await room;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            Task<List<Room>> rooms = Rooms.ToListAsync();
            return await rooms;
        }

        public async Task UpdateRoom(Room room)
        {
            Rooms.Update(room);
            await SaveChangesAsync();
        }

        public async Task DeleteRoom(long id)
        {
            Room room = GetRoom(id).Result;
            Rooms.Remove(room);
            await SaveChangesAsync();
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await Rooms
                .Include(room => room.Residents)
                .Where(room => !room.Residents.Any(resident => resident.PetType == PetType.Owl || resident.PetType == PetType.Cat))
                .ToListAsync();
        }

        public async Task<List<Room>> GetAvailableRooms()
        {
            return await Rooms
                .Where(room => room.Residents.Count < room.Capacity)
                .ToListAsync();
        }

        public async Task<List<Potion>> GetAllPotions()
        {
            Task<List<Potion>> potions = Potions.ToListAsync();
            return await potions;
        }

        public async Task<Student> AddStudentByRandomValues(string name)
        {
            Student student = new Student
            {
                Name = name,
                HouseType = (HouseType)_random.Next(0, 4),
                PetType = (PetType)_random.Next(0, 3),
                Room = await GetRoom(1)
            };

            await Students.AddAsync(student);
            return student;
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

        public async Task<Student> GetStudent(string studentName)
        {
            Task<Student> student = Students.FirstAsync(student => student.Name == studentName);
            return await student;
        }

        public async Task<Student> GetStudentById(long id)
        {
            Task<Student> student = Students.FirstAsync(student => student.ID == id);
            return await student;
        }

        public async Task<List<Potion>> GetStudentPotions(long studentId)
        {
            return await Potions
                .Where(potion => potion.Student.ID == studentId)
                .ToListAsync();
        }

        public async Task<Potion> AddPotionToStudent(long id)
        {
            Student student;

            try
            {
                student = await GetStudentById(id);
            }
            catch
            {
                student = await AddStudentByRandomValues($"{id}'s name");
            }

            Potion potion = new Potion
            {
                Student = student
            };

            await Potions.AddAsync(potion);
            await SaveChangesAsync();
            return potion;
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

        private async Task<Potion> AddIngredient(long id, Ingredient newIngredient)
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
    }
}
