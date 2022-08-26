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

            // Look for any students.
            if (context.Students.Any())
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

            context.SaveChanges();
        }
    }
}
