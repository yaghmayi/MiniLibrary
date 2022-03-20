using System.Collections.Generic;
using MiniLibrary.Models;

namespace MiniLibrary.DataAccess.Interfaces
{
	public interface IItemRepository
	{
		List<Item> GetAll();
		Item GetById(int id);
		void Add(Item item);
		void Update(Item item);
		void Delete(int id);
		int GetNextId();
	}
}
