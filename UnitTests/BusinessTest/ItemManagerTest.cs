using System.Collections.Generic;
using MiniLibrary.Business;
using MiniLibrary.Business.Interfaces;
using MiniLibrary.DataAccess;
using MiniLibrary.DataAccess.Interfaces;
using MiniLibrary.Models;
using Xunit;

namespace MiniLibrary.UnitTests.BusinessTest
{
	[Collection("Sequential")]
	public class ItemManagerTest
	{
		[Fact]
		public void GetAll()
		{
			IItemManager itemManager = new ItemManager(new ItemRepository());
			List<Item> items = itemManager.GetAll();

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
		public void GetById()
		{
			IItemManager itemManager = new ItemManager(new ItemRepository());
			Item item = itemManager.GetById(1);

			Assert.NotNull(item);
			Assert.Equal(1, item.Id);
			Assert.Equal("Alice in Wonderland", item.Name);
			Assert.NotNull(item.Category);
			Assert.Equal(101, item.Category.Code);
			Assert.Equal("Book", item.Category.Name);
			Assert.Equal("Lewis Carroll", item.Author);
			Assert.NotNull(item.Description);

			item = itemManager.GetById(800);
			Assert.Null(item);
		}

		[Fact]
		public void Add_Update_Delete()
		{
			IItemManager itemManager = new ItemManager(new ItemRepository());

			ICategoryRepository categoryRepository = new CategoryRepository();
			ICategoryManager categoryManager = new CategoryManager(categoryRepository);

			Item item = new Item()
							{
								Name = "New York Times"
							};
			item.Category = categoryManager.GetByName("Magazine");

			itemManager.SaveOrUpdate(item);

			Item createdItem = itemManager.GetById(3);
			Assert.NotNull(createdItem);
			Assert.Equal(3, createdItem.Id);
			Assert.Equal("New York Times", createdItem.Name);
			Assert.NotNull(item.Category);
			Assert.Equal(102, item.Category.Code);
			Assert.Equal("Magazine", item.Category.Name);
			Assert.Null(item.Author);
			Assert.Null(item.Description);

			createdItem.Name = "New York History";
			createdItem.Category = categoryManager.GetByName("DVD");
			createdItem.Description = "It is a fantastic documentary about New York City.";
			itemManager.SaveOrUpdate(createdItem);

			Item updatedItem = itemManager.GetById(3);
			Assert.NotNull(updatedItem);
			Assert.Equal(3, updatedItem.Id);
			Assert.Equal("New York History", updatedItem.Name);
			Assert.NotNull(updatedItem.Category);
			Assert.Equal(103, updatedItem.Category.Code);
			Assert.Equal("DVD", updatedItem.Category.Name);
			Assert.Null(updatedItem.Author);
			Assert.Equal("It is a fantastic documentary about New York City.", updatedItem.Description);

			itemManager.Delete(3);
			item = itemManager.GetById(3);
			Assert.Null(item);
		}
	}
}
