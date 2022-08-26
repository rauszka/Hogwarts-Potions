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

            // Look for any students,rooms,potions.
            if (context.Students.Any() || context.Rooms.Any() || context.Potions.Any())
            {
                return;  // Database has been seeded
            }

            var rooms = new Room[]
            {
                new Room
                {
                    Capacity = 5,
                    Residents = new HashSet<Student>
                    {
                        new Student
                        {
                            Name = "Draco",
                            HouseType = HouseType.Slytherin,
                            PetType = PetType.Owl
                        }
                    }
                },

                new Room
                {
                    Capacity = 5,
                    Residents = new HashSet<Student>
                    {
                        new Student
                        {
                            Name = "Harry",
                            HouseType = HouseType.Gryffindor,
                            PetType = PetType.Owl
                        }
                    }
                },

                new Room
                {
                    Capacity = 5,
                    Residents = new HashSet<Student>
                    {
                        new Student
                        {
                            Name = "Ron",
                            HouseType = HouseType.Gryffindor,
                            PetType = PetType.Rat
                        }
                    }
                },

            };

            foreach (Room room in rooms)
            {
                context.Rooms.Add(room);
            }

            var potions = new Potion[]
            {
                new Potion()
                {
                    BrewingStatus = BrewingStatus.Brew,
                    Name = "love-potion",
                    Student = new Student()
                    {
                        Name = "Hermione", 
                        HouseType = HouseType.Gryffindor,
                        PetType = PetType.Cat
                    },
                    Ingredients = {new Ingredient {Name = "valerian"}}
                }
            };

            foreach (Potion potion in potions)
            {
                context.Potions.Add(potion);
            }

            context.SaveChanges();
        }
    }
}
