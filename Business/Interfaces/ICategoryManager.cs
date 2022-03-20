using System.Collections.Generic;
using MiniLibrary.Models;

namespace MiniLibrary.Business.Interfaces
{
    public interface ICategoryManager
    {
        public List<Category> GetAll();
        public Category GetByName(string categoryName);
    }
}