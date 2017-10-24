using RecipeManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager.Web.DAL
{
    public interface IRecipeDAL
    {
        List<Recipe> GetLast20Recipes();
        void SaveNewRecipe(Recipe r);
        Recipe GetRecipe(int id);
    }
}
