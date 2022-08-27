using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Models.Implementations;

public class RecipeIMP : IRecipe
{
    private HogwartsContext _context;

    public RecipeIMP(HogwartsContext context)
    {
        _context = context;
    }

    public Task<Recipe> GetRecipe(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Recipe>> GetAllRecipes()
    {
        throw new System.NotImplementedException();
    }

    public Task AddRecipe(Recipe recipe)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteRecipe(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Recipe>> GetAllRecipesWithPotionIngredients(long potionId)
    {
        throw new System.NotImplementedException();
    }

    public Task ChangePotionStatus(Potion potion)
    {
        throw new System.NotImplementedException();
    }
}