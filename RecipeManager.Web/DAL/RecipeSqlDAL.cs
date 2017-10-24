using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeManager.Web.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace RecipeManager.Web.DAL
{
    public class RecipeSqlDAL : IRecipeDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["RecipeDB"].ConnectionString;

        const string SQL_GetTop20Recipes = "SELECT TOP 20 recipe.*, cuisinetype.* FROM recipe JOIN cuisinetype ON recipe.cuisinetypeid = cuisinetype.id  ORDER BY recipe.id DESC;";
        const string SQL_InsertNewRecipe = "INSERT INTO recipe VALUES(@name, @description, @cookingTime, @prepTime, @details, @imageName, 1);";
        const string SQL_GetRecipeById = "SELECT recipe.*, cuisinetype.* FROM recipe JOIN cuisinetype ON recipe.cuisinetypeid = cuisinetype.id WHERE recipe.id = @id;";

        public List<Recipe> GetLast20Recipes()
        {
            List<Recipe> recipes = new List<Recipe>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetTop20Recipes, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Recipe r = new Recipe();
                        r.Id = Convert.ToInt32(reader["id"]);
                        r.Name = Convert.ToString(reader["name"]);
                        r.Description = Convert.ToString(reader["description"]);
                        r.CookingTime = Convert.ToInt32(reader["cookingTime"]);
                        r.PrepTime = Convert.ToInt32(reader["prepTime"]);
                        r.Details = Convert.ToString(reader["details"]);
                        r.ImageName = Convert.ToString(reader["imageName"]);
                        r.CuisineTypeId = Convert.ToInt32(reader["cuisineTypeId"]);
                        r.CuisineType = Convert.ToString(reader["cuisineType"]);

                        recipes.Add(r);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return recipes;
        }

        public Recipe GetRecipe(int id)
        {
            Recipe recipe = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetRecipeById, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Recipe r = new Recipe();
                        r.Id = Convert.ToInt32(reader["id"]);
                        r.Name = Convert.ToString(reader["name"]);
                        r.Description = Convert.ToString(reader["description"]);
                        r.CookingTime = Convert.ToInt32(reader["cookingTime"]);
                        r.PrepTime = Convert.ToInt32(reader["prepTime"]);
                        r.Details = Convert.ToString(reader["details"]);
                        r.ImageName = Convert.ToString(reader["imageName"]);
                        r.CuisineTypeId = Convert.ToInt32(reader["cuisineTypeId"]);
                        r.CuisineType = Convert.ToString(reader["cuisineType"]);

                        recipe = r;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return recipe;
        }

        

        public void SaveNewRecipe(Recipe r)
        {
            try
            {
                // Create a Connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Create the SQL command with INSERT statement & params
                    SqlCommand cmd = new SqlCommand(SQL_InsertNewRecipe, conn);
                    cmd.Parameters.AddWithValue("@name", r.Name);
                    cmd.Parameters.AddWithValue("@description", r.Description);
                    cmd.Parameters.AddWithValue("@cookingTime", r.CookingTime);
                    cmd.Parameters.AddWithValue("@prepTime", r.PrepTime);
                    cmd.Parameters.AddWithValue("@details", r.Details);

                    //25. Support optional image path
                    if (String.IsNullOrEmpty(r.ImageName))
                    {
                        cmd.Parameters.AddWithValue("@imageName", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@imageName", r.ImageName);
                    }



                    // Execute the command
                    cmd.ExecuteNonQuery();

                    // Return back to controller
                    return;
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
    }
}