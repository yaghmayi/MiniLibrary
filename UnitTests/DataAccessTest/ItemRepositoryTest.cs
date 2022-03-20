using System.Collections.Generic;
using MiniLibrary.DataAccess;
using MiniLibrary.DataAccess.Interfaces;
using MiniLibrary.Models;
using Xunit;

namespace UnitTests.DataAccessTest
{
	[Collection("Sequential")]
	public class ItemRepositoryTest
	{
		[Fact]
		public void GetAll()
		{
			IItemRepository itemRepository = new ItemRepository();
			List<Item> items = itemRepository.GetAll();

			Assert.Collection(items,
							  item =>
							  {
								  Assert.Equal(1, item.Id);
								  Assert.Equal("Alice in Wonderland", item.Name);
								  Assert.NotNull(item.Category);
								  Assert.Equal(101, item.Category.Code);
								  Assert.Equal("Book", item.Category.Name);
							  },
							  item =>
							  {
								  Assert.Equal(2, item.Id);
								  Assert.Equal("Harry Potter", item.Name);
								  Assert.NotNull(item.Category);
								  Assert.Equal(101, item.Category.Code);
								  Assert.Equal("Book", item.Category.Name);
							  });
		}

		[Fact]
		public void GetById()
		{
			IItemRepository itemRepository = new ItemRepository();
			Item item = itemRepository.GetById(1);

			Assert.NotNull(item);
			Assert.Equal(1, item.Id);
			Assert.Equal("Alice in Wonderland", item.Name);
			Assert.NotNull(item.Category);
			Assert.Equal(101, item.Category.Code);
			Assert.Equal("Book", item.Category.Name);

			item = itemRepository.GetById(800);
			Assert.Null(item);
		}

		[Fact]
		public void Add_Update_Delete()
		{
			IItemRepository itemRepository = new ItemRepository();
			ICategoryRepository categoryRepository = new CategoryRepository();

			Item item = new Item()
							{
								Id = 3,
								Name = "New York Times"
							};
			item.Category = categoryRepository.GetByName("Magazine");

			itemRepository.Add(item);

			Item createdItem = itemRepository.GetById(3);
			Assert.NotNull(createdItem);
			Assert.Equal(3, createdItem.Id);
			Assert.Equal("New York Times", createdItem.Name);
			Assert.NotNull(item.Category);
			Assert.Equal(102, item.Category.Code);
			Assert.Equal("Magazine", item.Category.Name);


			createdItem.Name = "New York History";
			createdItem.Category = categoryRepository.GetByName("DVD");
			itemRepository.Update(createdItem);

			Item updatedItem = itemRepository.GetById(3);
			Assert.NotNull(updatedItem);
			Assert.Equal(3, updatedItem.Id);
			Assert.Equal("New York History", updatedItem.Name);
			Assert.NotNull(updatedItem.Category);
			Assert.Equal(103, updatedItem.Category.Code);
			Assert.Equal("DVD", updatedItem.Category.Name);

			itemRepository.Delete(3);
			item = itemRepository.GetById(3);
			Assert.Null(item);
		}

		[Fact]
		public void GetNextId()
		{
			IItemRepository itemRepository = new ItemRepository();
			Assert.Equal(3, itemRepository.GetNextId());
		}
	}
}
