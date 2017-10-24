using RecipeManager.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeManager.Web.Controllers
{
    //TODO 2. Create a Home Controller to serve up the home page
    public class HomeController : Controller
    {
        //TODO 3. Add a constructor to support the dependency injection
        private IRecipeDAL dal;
        public HomeController(IRecipeDAL dal) // inject the IRecipeDAL
        {
            this.dal = dal;
        }

        // GET: Home
        [HttpGet] //By default, everything we do is an HttpGet
        public ActionResult Index()
        {
            //TODO 4. Return the Index View
            //return View("Index");

            //TODO 6. Go to the database and get the recent 20 recipes
            //      and pass the List<Recipe> as the model
            var recipes = dal.GetLast20Recipes();
            return View("Index", recipes);
        }
    }
}