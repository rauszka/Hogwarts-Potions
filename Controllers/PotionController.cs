using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/potion")]
    public class PotionController : ControllerBase
    {
        private readonly IPotionService _context;

        public PotionController(IPotionService context)
        {
            _context = context;
        }

        [HttpGet("/potions")]
        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.GetAllPotions();
        }

        [HttpPost("/potions")]
        public async Task<List<Potion>> Potions()
        {
            throw new NotImplementedException();
        }

    }
}
