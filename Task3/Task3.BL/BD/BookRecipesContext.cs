using System.Data.Entity;
using Task2.BL.Model;

namespace Task2.BL.DAL
{
    public class BookRecipesContext : DbContext
    {
        public BookRecipesContext():base("DBConnection"){}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
     //   public DbSet<Recipe> Recipes { get; set; }
     //   public DbSet<Ingredient> Ingredients { get; set; }
    }
}
