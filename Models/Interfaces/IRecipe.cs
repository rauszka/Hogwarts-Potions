using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Models.Entities;

public interface IRecipe
{
    Task<Recipe> GetRecipe(long id);
    Task<List<Recipe>> GetAllRecipes();
    Task AddRecipe(Recipe recipe);
    Task DeleteRecipe(long id);
    Task<List<Recipe>> GetAllRecipesWithPotionIngredients(long potionId);
    Task ChangePotionStatus(Potion potion);
}