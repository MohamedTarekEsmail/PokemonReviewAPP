using PokemonReviewApp.Model;

namespace PokemonReviewApp.Interface
{
    public interface IReviewRepository
    {
        Review GetReview(int reviewId);
        ICollection<Review> GetReviews();
        ICollection<Review> GetReviewsOfAPokemon(int pokeId);
        //ICollection<Review> GetReviewByReviewer(int reviewerId);
        bool ReviewExists(int id);
    }
}
