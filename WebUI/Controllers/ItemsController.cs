using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MiniLibrary.Business;
using MiniLibrary.Business.Interfaces;
using MiniLibrary.Models;

namespace MiniLibrary.WebUI.Controllers
{
	[Route("minilibrary/api/items")]
	public class ItemsController : Controller
	{
		
		private readonly IItemManager _itemManager;
		private readonly ICategoryManager _categoryManager;

		public ItemsController(IItemManager itemManager, ICategoryManager categoryManager)
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
