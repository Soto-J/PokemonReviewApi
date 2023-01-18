using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class OwnerRepository : IOwnerRepository
	{
		private readonly DataContext _context;

		public OwnerRepository(DataContext context)
		{
			_context = context;
		}

		public bool CreateOwner(Owner owner)
		{
			_context.Add(owner);
			return Save();
		}

		public Owner? GetOwner(int ownerId) => 
			_context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();

		public ICollection<Owner?> GetOwnerOfPokemon(int pokeId) =>
			_context.PokemonOwners
				.Where(po => po.Pokemon.Id == pokeId)
				.Select(o => o.Owner)
				.ToList();

		public ICollection<Owner> GetOwners() =>
			_context.Owners.ToList();

		public ICollection<Pokemon?> GetPokemonByOwner(int ownerId) =>
			_context.PokemonOwners.Where(po => po.Owner.Id == ownerId)
				.Select(p => p.Pokemon)
				.ToList();

		public bool OwnerExists(int ownerId) =>
			_context.Owners.Any(o => o.Id == ownerId);

		public bool Save() => _context.SaveChanges() > 0;
	}
}
