using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeManager.Web.Models
{
    public class Recipe
    {
        //TODO 17. Add the Display Attributes to make labels user-friendly

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Cooking Time (in minutes)")]
        public int CookingTime { get; set; }

        [Display(Name = "Preparation Time (in minutes)")]
        public int PrepTime { get; set; }

        [Display(Name = "Steps")]
        public string Details { get; set; }
        public string ImageName { get; set; }

        // Related Properties thru Join Table
        public int CuisineTypeId { get; set; }
        public string CuisineType { get; set; }
    }
}