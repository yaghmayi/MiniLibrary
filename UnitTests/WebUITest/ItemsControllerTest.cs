using System.Collections.Generic;
using System.Linq;
using MiniLibrary.Business;
using MiniLibrary.Business.Interfaces;
using MiniLibrary.DataAccess;
using MiniLibrary.Models;
using MiniLibrary.WebUI.Controllers;
using Xunit;

namespace MiniLibrary.UnitTests.WebUITest
{
	[Collection("Sequential")]
	public class ItemsControllerTest
	{
		[Fact]
		public void GetAll()
		{
			ItemsController itemsController = CreateItemsController();

			List<Item> items = itemsController.GetAll();

			Assert.Collection(items,
							  item =>
							  {
								  Assert.Equal(1, item.Id);
								  Assert.Equal("Alice in Wonderland", item.Name);
								  Assert.NotNull(item.Category);
								  Assert.Equal(101, item.Category.Code);
								  Assert.Equal("Book", item.Category.Name);
								  Assert.Equal("Lewis Carroll", item.Author);
								  Assert.NotNull(item.Description);
							  },
							  item =>
							  {
								  Assert.Equal(2, item.Id);
								  Assert.Equal("Harry Potter", item.Name);
								  Assert.NotNull(item.Category);
								  Assert.Equal(101, item.Category.Code);
								  Assert.Equal("Book", item.Category.Name);
								  Assert.Equal("J. K. Rowling", item.Author);
								  Assert.NotNull(item.Description);
							  });

		}

		[Fact]
		public void GetCategories()
		{
			ItemsController itemsController = CreateItemsController();

			List<Category> categories = itemsController.GetCategories();
			Assert.Collection(categories,
							  category =>
							  {
								  Assert.Equal(101, category.Code);
								  Assert.Equal("Book", category.Name);
							  },
							  category =>
							  {
								  Assert.Equal(103, category.Code);
								  Assert.Equal("DVD", category.Name);
							  },
							  category =>
							  {
								  Assert.Equal(102, category.Code);
								  Assert.Equal("Magazine", category.Name);
							  });
		}


		[Fact]
		public void Post_Update_Delete()
		{
			ItemsController itemsController = CreateItemsController();

			Item item = new Item()
						{
							Name = "Mother Earth News"
			};
			List<Category> categories = itemsController.GetCategories();
			item.Category = categories.First(x => x.Name.Equals("Magazine"));
			
			itemsController.Post(item);

			Item createdItem = itemsController.GetById(3);
			Assert.NotNull(createdItem);
			Assert.Equal(3, createdItem.Id);
			Assert.Equal("Mother Earth News", createdItem.Name);
			Assert.NotNull(item.Category);
			Assert.Equal(102, item.Category.Code);
			Assert.Equal("Magazine", item.Category.Name);
			Assert.Null(item.Author);
			Assert.Null(item.Description);

			createdItem.Name = "Mother Earth";
			createdItem.Category = categories.First(x => x.Name.Equals("DVD"));
			createdItem.Description = "It is a fantastic documentary about Earth.";
			itemsController.Post(createdItem);

			Item updatedItem = itemsController.GetById(3);
			Assert.NotNull(updatedItem);
			Assert.Equal(3, updatedItem.Id);
			Assert.Equal("Mother Earth", updatedItem.Name);
			Assert.NotNull(updatedItem.Category);
			Assert.Equal(103, updatedItem.Category.Code);
			Assert.Equal("DVD", updatedItem.Category.Name);
			Assert.Null(updatedItem.Author);
			Assert.Equal("It is a fantastic documentary about Earth.", updatedItem.Description);

			itemsController.Delete(3);
			item = itemsController.GetById(3);
			Assert.Null(item);
		}

		private ItemsController CreateItemsController()
		{
			IItemManager itemManager = new ItemManager(new ItemRepository());
			ICategoryManager categoryManager = new CategoryManager(new CategoryRepository());

			ItemsController itemsController = new ItemsController(itemManager, categoryManager);

			return itemsController;
		}
	}
}
