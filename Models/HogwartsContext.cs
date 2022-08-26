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

        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Room>().ToTable("Room");
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
    }
}
