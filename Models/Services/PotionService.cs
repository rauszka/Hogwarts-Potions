using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models
{
    public class PotionService : IPotionService
    {
        private HogwartsContext _context;

        public PotionService(HogwartsContext context)
        {
            _context = context;
        }

        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.Potions.ToListAsync();
        }

        public async Task AddPotion(Potion potion)
        {
            await _context.Potions.AddAsync(potion);
            await _context.SaveChangesAsync();
        }

        public async Task<Potion> GetPotion(long id)
        {
            return await _context.Potions
                .Include(pot => pot.Recipe)
                .FirstOrDefaultAsync(potion => potion.Id == id);
        }

        public void UpdatePotion(Potion potion)
        {
            _context.Potions.Update(potion);
            _context.SaveChanges();
        }

        public async Task DeletePotion(long id)
        {
            Potion potion = await GetPotion(id);
            await _context.Potions.Where(p => p.Id == id).LoadAsync();
            _context.Potions.Remove(potion);
            await _context.SaveChangesAsync();
        }
    }
}