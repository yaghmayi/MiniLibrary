using System.Collections.Generic;
using System.Linq;
using MiniLibrary.DataAccess.Interfaces;
using MiniLibrary.Models;

namespace MiniLibrary.Business
{
    public class ItemManager : IItemManager
	{
		private readonly IItemRepository _itemRepository;

		public ItemManager(IItemRepository itemRepository)
		{
			_itemRepository = itemRepository;
		}


		public List<Item> GetAll()
		{
			List<Item> items = _itemRepository.GetAll()
												   .OrderBy(x => x.Id)
												   .ToList();

			return items;
		}

		public Item GetById(int id)
		{
			Item item = _itemRepository.GetById(id);

			return item;
		}

		public void SaveOrUpdate(Item item)
		{
            if (item.Id <= 0)
            {
                item.Id = _itemRepository.GetNextId();
				_itemRepository.Add(item);
			}
            else
            {
                _itemRepository.Update(item);
            }
		}

		public void Delete(int id)
		{
			_itemRepository.Delete(id);
		}
	}
}