using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewerController : Controller
	{
		private readonly IReviewerRepository _reviewerRepository;
		private readonly IMapper _mapper;

		public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
		{
			this._reviewerRepository = reviewerRepository;
			this._mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
		public IActionResult GetReviewers()
		{
			var reviewers = this._mapper
				.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(reviewers);
		}

		[HttpGet("{reviewerId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(Reviewer))]
		public IActionResult GetReviewer(int reviewerId)
		{
			if (!this._reviewerRepository.ReviewerExist(reviewerId))
				return NotFound();

			var reviewer = this._mapper
				.Map<ReviewerDto>(this._reviewerRepository.GetReviewer(reviewerId));

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(reviewer);
		}

		[HttpGet("{reviewerId}/reviews")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		public IActionResult GetReviewsByReviewer(int reviewerId)
		{
			if (!this._reviewerRepository.ReviewerExist(reviewerId)) 
				return NotFound();

			var reviews = this._mapper
				.Map<List<ReviewDto>>(this._reviewerRepository.GetReviewsByReviewer(reviewerId));

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(reviews);
		}
	}
}
