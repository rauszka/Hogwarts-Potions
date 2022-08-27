using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Models.Implementations;

public class IngredientIMP : IIngredient
{
    private HogwartsContext _context;

    public IngredientIMP(HogwartsContext context)
    {
        _context = context;
    }
    public Task<Ingredient> GetIngredient(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Ingredient>> GetAllIngredients()
    {
        throw new System.NotImplementedException();
    }

    public Task AddIngredient(Ingredient ingredient)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteIngredient(long id)
    {
        throw new System.NotImplementedException();
    }
}