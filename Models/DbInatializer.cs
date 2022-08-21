using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

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
                return;   // DB has been seeded
            }

            Student student = new Student() { HouseType = HouseType.Ravenclaw, PetType = PetType.None, Name = "Hagrid" };

            Room room = new Room() {Capacity = 5,Residents = new HashSet<Student>(){student}};

            var rooms = new Room[] { room };

            foreach (Room ro in rooms)
            {
                context.Rooms.Add(ro);
            }
            context.SaveChanges();
            
        }
    }
}