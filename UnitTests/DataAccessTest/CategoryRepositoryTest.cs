using System.Collections.Generic;
using MiniLibrary.DataAccess;
using MiniLibrary.DataAccess.Interfaces;
using MiniLibrary.Models;
using Xunit;

namespace MiniLibrary.UnitTests.DataAccessTest
{
	[Collection("Sequential")]
	public class CategoryRepositoryTest
	{
		[Fact]
		public void GetAll()
		{
			ICategoryRepository categoryRepository = new CategoryRepository();
			List<Category> categories = categoryRepository.GetAll();

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
							  }
							 );
		}


		[Fact]
		public void GetByName()
		{
			ICategoryRepository categoryRepository = new CategoryRepository();

			Category category = categoryRepository.GetByName("Magazine");
			Assert.NotNull(category);
			Assert.Equal(102, category.Code);
			Assert.Equal("Magazine", category.Name);

			category = categoryRepository.GetByName("NotExistName");
			Assert.Null(category);
		}
	}
}
