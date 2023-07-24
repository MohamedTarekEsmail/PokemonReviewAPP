using PokemonReviewApp.Model;

namespace PokemonReviewApp.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonByCategory(int categoryid);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool Save();
    }
}
