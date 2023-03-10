using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : Controller
	{
		private readonly IReviewRepository _reviewRepository;
		private readonly IMapper _mapper;

		public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
		{
			this._reviewRepository = reviewRepository;
			this._mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		public IActionResult GetReviews()
		{
			var reviews = this._mapper
				.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(reviews);
		}

		[HttpGet("{reviewId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(Review))]
		public IActionResult GetReview(int reviewId)
		{
			if (!this._reviewRepository.ReviewExists(reviewId))
				return NotFound();

			var review = this._mapper
				.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(review);
		}

		[HttpGet("pokemon/{pokeId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		public IActionResult GetReviewsOfPokemon(int pokeId)
		{
			var reviews = this._mapper
				.Map<List<ReviewDto>>(this._reviewRepository.GetReviewsOfPokemon(pokeId));

			if (!ModelState.IsValid) return BadRequest(ModelState);
			
			return Ok(reviews);
		}
	}
}
