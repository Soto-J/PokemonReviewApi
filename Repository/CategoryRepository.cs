using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DataContext _context;

		public CategoryRepository(DataContext context)
		{
			_context = context;
		}

		public bool CategoryExists(int categoryId)
		{
			return _context.Categories.Any(c => c.Id == categoryId);
		}

		public bool CreateCategory(Category category)
		{
			// .Add Starts Change Tracker - Add, Updating, Modifying
			_context.Add(category);
			
			return Save();
		}

		public ICollection<Category> GetCategories()
		{
			return _context.Categories.ToList();
		}

		public Category GetCategory(int categoryId)
		{
			return _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
		}

		public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
		{
			return _context.PokemonCategories.Where(pc => pc.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
