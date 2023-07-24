﻿using PokemonReviewApp.Data;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Model;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;
        public CategoryRepository( DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c=>c.ID== id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e => e.ID == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryid)
        {
            return _context.PokemonCategories.Where(e => e.CategoryID == categoryid).Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
           var saved =  _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}