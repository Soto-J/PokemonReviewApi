using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : Controller
	{
		private readonly IOwnerRepository _ownerRepository;
		private readonly IMapper _mapper;

		public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
		{
			_ownerRepository = ownerRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
		public IActionResult GetOwners()
		{
			var owners = _mapper
				.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(owners);
		}

		[HttpGet("{ownerId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(Owner))]
		public IActionResult GetOwner(int ownerId)
		{
			if (!_ownerRepository.OwnerExists(ownerId)) return NotFound();

			var owner = _mapper
				.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(owner);
		}

		[HttpGet("{ownerId}/pokemon")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(Pokemon))]
		public IActionResult GetPokemonByOwner(int ownerId)
		{
			if (!_ownerRepository.OwnerExists(ownerId)) return NotFound();

			var pokemon = _mapper
				.Map<List<PokemonDto>>(_ownerRepository.GetPokemonByOwner(ownerId));

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(pokemon);
		}

		//[HttpGet("{pokemonId}/pokemon")]
		//[ProducesResponseType(400)]
		//[ProducesResponseType(200, Type = typeof(Owner))]
		//public IActionResult GetOwnerOfPokemon(int pokemonId)
		//{
		//	var owner = _mapper
		//		.Map<IEnumerable<OwnerDto>>(_ownerRepository.GetOwnerOfPokemon(pokemonId));

		//	if (!ModelState.IsValid) return BadRequest(ModelState);

		//	return Ok(owner);
		//}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateOwner([FromBody] OwnerDto ownerCreate)
		{
			if (ownerCreate == null)
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var owner = _ownerRepository.GetOwners()
				.Where(o =>
				{
					var ownerFirstName = o.FirstName.Trim().ToUpper();
					var ownerLastName = o.LastName.Trim().ToUpper();
					var createFirstName = ownerCreate.FirstName.Trim().ToUpper();
					var createLastName = ownerCreate.LastName.Trim().ToUpper();

					return ownerFirstName == createFirstName && ownerLastName == createLastName;
				})
				.FirstOrDefault();

			if (owner != null)
			{
				ModelState.AddModelError("", "Category already exists");
				return StatusCode(422, ModelState);
			}

			var ownerMap = _mapper.Map<Owner>(ownerCreate);

			if (!_ownerRepository.CreateOwner(ownerMap))
			{
				ModelState.AddModelError("", "Something went wrong whle saving");
				return StatusCode(500, ModelState);
			}

			return Ok("Success");
		}
	}
}
