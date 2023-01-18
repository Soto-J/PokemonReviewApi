using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class ReviewerRepository : IReviewerRepository
	{
		private readonly DataContext _context;

		public ReviewerRepository(DataContext context)
		{
			this._context = context;
		}

		public Reviewer GetReviewer(int reviewerId) =>
			this._context.Reviewers
				.Where(reviewer => reviewer.Id == reviewerId)
				.Include(e => e.Reviews)
				.FirstOrDefault();

		public ICollection<Reviewer> GetReviewers() =>
			this._context.Reviewers.ToList();


		public ICollection<Review> GetReviewsByReviewer(int reviewerId) =>
			this._context.Reviews
				.Where(reviews => reviews.Reviewer.Id == reviewerId)
				.ToList();

		public bool ReviewerExist(int reviewerId) =>
			this._context.Reviewers.Any(reviewer => reviewer.Id == reviewerId);
	}
}
