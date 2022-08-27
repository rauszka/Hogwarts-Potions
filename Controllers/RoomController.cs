using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoom _roomImp;

        public RoomController(IRoom room)
        {
            _roomImp = room;
        }

        [HttpGet]
        public async Task<List<Room>> GetAllRooms()
        {
            return await _roomImp.GetAllRooms();
        }

        [HttpPost]
        public async Task AddRoom([FromBody] Room room)
        {
            await _roomImp.AddRoom(room);
        }

        [HttpGet("/{id}")]
        public async Task<Room> GetRoomById(long id)
        {
            return await _roomImp.GetRoom(id);
        }

        [HttpPut("/{id}")]
        public async void UpdateRoomById(long id, [FromBody] Room updatedRoom)
        {
           updatedRoom.ID = id;
           await _roomImp.UpdateRoom(updatedRoom);
        }

        [HttpDelete("/{id}")]
        public async Task DeleteRoomById(long id)
        {
            await _roomImp.DeleteRoom(id);
        }

        [HttpGet("/rat-owners")]
        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _roomImp.GetRoomsForRatOwners();
        }

        [HttpGet("/rat-owners")]
        public async Task<List<Room>> GetAvailableRooms()
        {
            return await _roomImp.GetAvailableRooms();
        }

    }
}
