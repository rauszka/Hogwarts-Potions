using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace HogwartsPotions.Models
{
    public interface IPotionService
    {
        Task<List<Potion>> GetAllPotions();
        Task AddPotion(Potion potion);
        Task<Potion> GetPotion(long id);
        void UpdatePotion(Potion potion);
        Task DeletePotion(long id);
    }
}