using RecipeManager.Web.Attributes;
using RecipeManager.Web.DAL;
using RecipeManager.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeManager.Web.Controllers
{
    //TODO 9. Create a Recipes Controller to allow
    //      the user to view all recipes, create a new recipe
    //      see detail for a specific recipe, and also edit a recipe (MAYBE) 
    public class RecipesController : Controller
    {
        //TODO 10. Create a constructor to inject the IRecipeDAL
        //      because of Ninject, we are going to get a RecipeSqlDAL
        private IRecipeDAL dal;
        public RecipesController(IRecipeDAL dal)
        {
            this.dal = dal;
        }

        //TODO 29. Add the Detail Action to get a recipe from the database
        public ActionResult Detail(int id = 0) //defaults recipeId to 0 so user doesn't get an error if they miss it
        {
            Recipe r = dal.GetRecipe(id);

            if (r == null)
            {
                return HttpNotFound();
            }

            return View("Detail", r);
        }

        //TODO 11. Create the New action (GET) to display the empty HTML
        [HttpGet]
        public ActionResult New(bool draftMode = false)
        {
            //TODO 28. Get the current recipe out of session, if available and pass to the view
            if (draftMode && Session["CurrentRecipe"] != null)
            {
                Recipe r = Session["CurrentRecipe"] as Recipe;
                return View("New", r);
            }
            else
            {
                return View("New");
            }
        }

        //TODO 26. Create an action that will be able to save our recipe into Session
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SaveForLater")]
        public ActionResult SaveForLater(Recipe r, HttpPostedFileBase recipeImage)
        {
            //TODO 27. Add the inprogress recipe to Session
            Session["CurrentRecipe"] = r;

            return RedirectToAction("Index", "Home");
        }

        //TODO 12. Create the New action (POST) to "save" the data 
        //      and redirect to the Home screen
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "New")]
        public ActionResult New(Recipe r, HttpPostedFileBase recipeImage) //23. Add a parameter to support the image
        {
            ////TODO 13. Redirect to home page
            //return RedirectToAction("Index", "Home"); //--> Home/Index

            //TODO 18. Save the recipe to the database
            //      then redirect back to home
            //dal.SaveNewRecipe(r);
            //return RedirectToAction("Index", "Home");

            //TODO 24. Save the image the filesystem
            //      then save the recipe to the database
            if (recipeImage != null)
            {
                // Generate the final file path
                //string filename = Path.GetFileName(recipeImage.FileName);
                string extension = Path.GetExtension(recipeImage.FileName);
                string filename = Guid.NewGuid() + extension;
                string placeToSaveIt = Path.Combine(Server.MapPath("~/images/"), filename);

                // Save the image to the disk
                recipeImage.SaveAs(placeToSaveIt);
                r.ImageName = filename;
            }

            dal.SaveNewRecipe(r);

            //TODO 29. Reset CurrentRecipe to null
            Session["CurrentRecipe"] = null;

            return RedirectToAction("Index", "Home");
        }

    }
}