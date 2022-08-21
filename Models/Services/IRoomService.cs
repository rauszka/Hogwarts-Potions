using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace HogwartsPotions.Models
{
    public interface IRoomService
    {
        Task AddRoom(Room room);
        Task<Room> GetRoom(long roomId);
        Task<List<Room>> GetAllRooms();
        void UpdateRoom(Room room);
        Task DeleteRoom(long id);
        Task<List<Room>> GetRoomsForRatOwners();
    }
}