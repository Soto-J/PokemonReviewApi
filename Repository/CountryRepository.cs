using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class CountryRepository : ICountryRepository
	{
		private readonly DataContext _context;

		public CountryRepository(DataContext context)
		{
			_context = context;
		}

		public bool CountryExist(int countryId) =>
			_context.Countries.Any(c => c.Id == countryId);

		public bool CreateCountry(Country country)
		{
			_context.Countries.Add(country);
			return Save();
		}

		public ICollection<Country> GetCountries() =>
			_context.Countries.ToList();

		public Country GetCountry(int countryId) =>
			_context.Countries.Where(c => c.Id == countryId).FirstOrDefault();

		public Country GetCountryByOwner(int ownerId) => 
			_context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
		
		public ICollection<Owner> GetOwnersFromACountry(int countryId) => 
			_context.Owners.Where(c => c.Country.Id == countryId).ToList();

		public bool Save() => _context.SaveChanges() > 0;
	}
}
