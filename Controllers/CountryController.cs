using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : Controller
	{
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

		public CountryController(ICountryRepository countryRepository, IMapper mapper)
		{
			this._countryRepository = countryRepository;
			this._mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
		public IActionResult GetCountries()
		{
			var countries = this._mapper.Map<List<CountryDto>>(
				this._countryRepository.GetCountries()
			);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(countries);
		}

		[HttpGet("{countryId}")]
		[ProducesResponseType(200, Type = typeof(Country))]
		[ProducesResponseType(400)]
		public IActionResult GetCountry(int countryId)
		{
			if (!this._countryRepository.CountryExist(countryId))
				return NotFound();

			var country = this._mapper.Map<CountryDto>(
				this._countryRepository.GetCountry(countryId)
			);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(country);
		}

		[HttpGet("/owners/{ownerId}")]
		[ProducesResponseType(200, Type = typeof(Country))]
		[ProducesResponseType(400)]
		public IActionResult GetCountryOfAnOwner(int ownerId)
		{
			var country = this._mapper.Map<CountryDto>(
				this._countryRepository.GetCountryByOwner(ownerId)
			);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(country);
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
		{
			// Check to see if input is empty.
			if (countryCreate == null)
				return BadRequest(ModelState);

			// Checks if input already exists.
			var country = _countryRepository.GetCountries()
				.Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper())
				.FirstOrDefault();

			if (country != null)
			{
				ModelState.AddModelError("", "Country already exists.");
				return StatusCode(422, ModelState);
			}

			// Map input
			var countryMap = _mapper.Map<Country>(countryCreate);
			// Throw error if save is not successful.
			if (!_countryRepository.CreateCountry(countryMap))
			{
				ModelState.AddModelError("", "Something went wrong while saving.");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created.");
		}
	}
}
