using System.Collections.Generic;
using MiniLibrary.Models;

namespace MiniLibrary.DataAccess.Interfaces
{
	public interface ICategoryRepository
	{
		List<Category> GetAll();
		Category GetByName(string categoryName);
	}
}
