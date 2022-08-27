using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace HogwartsPotions.Models.Entities
{
    public interface IRoom
    {
        Task AddRoom(Room room);
        Task<Room> GetRoom(long roomId);
        Task<List<Room>> GetAllRooms();
        Task UpdateRoom(Room room);
        Task DeleteRoom(long id);
        Task<List<Room>> GetRoomsForRatOwners();
        Task<List<Room>> GetAvailableRooms();
    }
}
