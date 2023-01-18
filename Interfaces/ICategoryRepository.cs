using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();
		Category GetCategory(int categoryId);
		ICollection<Pokemon> GetPokemonByCategory(int categoryId);
		bool CategoryExists(int categoryId);
		bool CreateCategory(Category category);
		bool Save();
	}
}
