using System.Collections.Generic;
using MiniLibrary.Business.Interfaces;
using MiniLibrary.DataAccess.Interfaces;
using MiniLibrary.Models;

namespace MiniLibrary.Business
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryManager)
        {
            this._categoryRepository = categoryManager;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = _categoryRepository.GetAll();

            return categories;
        }

        public Category GetByName(string categoryName)
        {
            Category category = _categoryRepository.GetByName(categoryName);

            return category;
        }
    }
}