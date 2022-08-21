using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models
{
    public class RoomService : IRoomService
    {
        private HogwartsContext _context;

        public RoomService(HogwartsContext context)
        {
            _context = context;
        }
        public async Task AddRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(long id)
        {
            Room room = await GetRoom(id);
            await _context.Students.Where(s => s.Room == room).LoadAsync();
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.Rooms
                .Include(r => r.Residents)
                .ToListAsync();
        }
        
        public async Task<Room> GetRoom(long roomId)
        {
            return await _context.Rooms
                .Include(room => room.Residents)
                .FirstOrDefaultAsync(r => r.ID == roomId);
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _context.Rooms
                .Include(r =>r.Residents)
                .Where(r=>r.Residents.Any(resident => resident.PetType == PetType.Cat || resident.PetType == PetType.Owl)).ToListAsync();
        }

        public void UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
            //return Task.CompletedTask; return value Task instead of void
        }

    }
}