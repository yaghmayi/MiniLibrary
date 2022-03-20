using System.Collections.Generic;
using MiniLibrary.Models;

namespace MiniLibrary.Business
{
    public interface IItemManager
	{
		List<Item> GetAll();
		Item GetById(int id);
		void SaveOrUpdate(Item item);
		void Delete(int id);
	}
}