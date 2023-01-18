using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class ReviewRepository : IReviewRepository
	{
		private readonly DataContext _context;

		public ReviewRepository(DataContext context)
		{
			this._context = context;
		}

		public Review GetReview(int reviewId)
		{
			var review = this._context.Reviews
				.Where(r => r.Id == reviewId)
				.FirstOrDefault();

			return review;
		}

		public ICollection<Review> GetReviews() =>
			this._context.Reviews.ToList();

		public ICollection<Review> GetReviewsOfPokemon(int pokeId)
		{
			var pokemonReviews = this._context.Reviews
				.Where(review => review.Id == pokeId)
				.ToList();

			return pokemonReviews;
		}

		public bool ReviewExists(int reviewId) =>
			this._context.Reviews
			.Any(r => r.Id == reviewId);
	}
}
