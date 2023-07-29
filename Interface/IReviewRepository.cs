using PokemonReviewApp.Model;

namespace PokemonReviewApp.Interface
{
    public interface IReviewRepository
    {
        Review GetReview(int reviewId);
        ICollection<Review> GetReviews();
        ICollection<Review> GetReviewsOfAPokemon(int pokeId);
        bool ReviewExists(int id);
        bool CreateReview (Review review);
        bool Save();

    }
}
