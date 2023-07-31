using PokemonReviewApp.Data;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Model;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context) 
        {
           _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokeOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var pokeCategoryEntity = _context.Categories.Where(a => a.ID == categoryId).FirstOrDefault();

            var PokemonOwner = new PokemonOwner()
                {
                    Owner = pokeOwnerEntity,
                    Pokemon = pokemon,
                };
            
            _context.Add(PokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = pokeCategoryEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int PokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == PokeId);
            if (review.Count() <= 0)
                return 0;
            
            return ((decimal)review.Sum(r=>r.Rating)/review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p=>p.Id).ToList();
        }

        public bool PokemonExists(int PokeId)
        {
            return _context.Pokemons.Any(p => p.Id == PokeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges() ;
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
    }
}
