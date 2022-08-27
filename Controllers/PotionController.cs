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
        private readonly HogwartsContext _context;

        public PotionController(HogwartsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.GetAllPotions();
        }

        [HttpPost("{studentName}")]
        public async Task<Potion> AddPotion(string studentName, [FromBody] Potion potion)
        {
            Student student;

            try
            {
                student = await _context.GetStudent(studentName);
                potion.Student = student;
            }
            catch
            {
                student = await _context.AddStudentByRandomValues(studentName);
                potion.Student = student;
            }

            Potion newPotion = await _context.AddPotion(potion);
            await _context.SaveChangesAsync();
            return newPotion;
        }

        [HttpGet("{studentId}")]
        public async Task<List<Potion>> GetStudentPotions(long studentId)
        {
            return await _context.GetStudentPotions(studentId);
        }

        [HttpPost("brew/{studentId}")]
        public async Task<Potion> BrewPotion(long studentId)
        {
            return await _context.AddPotionToStudent(studentId);
        }

    }
}
