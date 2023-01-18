using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface ICountryRepository
	{
		ICollection<Country> GetCountries();
		Country GetCountry(int countryId);
		Country GetCountryByOwner(int ownerId);
		ICollection<Owner> GetOwnersFromACountry(int countryId);
		bool CountryExist(int countryId);
		bool CreateCountry(Country country);
		bool Save();
	}
}
