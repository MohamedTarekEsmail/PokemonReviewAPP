namespace PokemonReviewApp.Model
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
    }
}
