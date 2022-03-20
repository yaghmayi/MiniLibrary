using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MiniLibrary.Business;
using MiniLibrary.Models;

namespace MiniLibrary.UI.Controllers
{
	[Route("minilibrary/api/items")]
	public class ItemsController : Controller
	{
		
		private readonly ItemManager _itemManager;
		private readonly CategoryManager _categoryManager;

		public ItemsController(ItemManager itemManager, CategoryManager categoryManager)
		{
			this._itemManager = itemManager;
            this._categoryManager = categoryManager;
        }

		[HttpGet]
		[Route("list")]
		public List<Item> GetAll()
		{
			List<Item> items = _itemManager.GetAll();

			return items;
		}

		[HttpGet]
		[Route("getById/{id:int}")]
		public Item GetById(int id)
		{
			Item item = _itemManager.GetById(id);

			return item;
		}

		[HttpPost]
		[Route("save")]
		public IActionResult Post([FromBody] Item item)
		{
			_itemManager.SaveOrUpdate(item);

			return Ok();
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public IActionResult Delete(int id)
		{
			_itemManager.Delete(id);

			return Ok();
		}

        [HttpGet]
        [Route("categories")]
        public List<Category> GetCategories()
        {
            List<Category> categories = _categoryManager.GetAll();

            return categories;
        }
	}
}
