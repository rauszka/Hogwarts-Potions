using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Models.Entities;

public interface IPotion
{
    Task<Potion> GetPotion(long potionId);
    Task<List<Potion>> GetAllPotions();
    Task AddPotion(Potion potion);
    Task DeletePotion(long id);
    Task<List<Potion>> GetAllPotionsByStudent(long studentId);
    Task<Potion> AddEmptyPotion(Student student);
    Task<Potion> AddIngredientToPotion(long potionId, Ingredient ingred);
}