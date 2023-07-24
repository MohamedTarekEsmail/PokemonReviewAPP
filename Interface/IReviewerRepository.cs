using PokemonReviewApp.Model;

namespace PokemonReviewApp.Interface
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId); 
        ICollection<Review> GetReviewsbyAReviewer(int reviewerId);
        bool ReviewerExists(int reviewerId);
    }
}
