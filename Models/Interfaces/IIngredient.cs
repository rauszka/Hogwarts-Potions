using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Models.Entities
{
    public interface IIngredient
    {
        Task<Ingredient> GetIngredient(long id);
        Task<List<Ingredient>> GetAllIngredients();
        Task AddIngredient(Ingredient ingredient);
        Task DeleteIngredient(long id);
    }
}
