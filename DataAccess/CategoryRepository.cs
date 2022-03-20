using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using MiniLibrary.DataAccess.Base;
using MiniLibrary.DataAccess.Interfaces;
using MiniLibrary.Models;

namespace MiniLibrary.DataAccess
{
	public class CategoryRepository : ICategoryRepository
	{
		private LibraryDb _libraryDb;

		public CategoryRepository()
		{
			_libraryDb = new LibraryDb();
			_libraryDb.Initialize();
		}

		public List<Category> GetAll()
		{
			List<Category> categories = new List<Category>();

			using (SQLiteConnection dbConnection = _libraryDb.GetDbConnection())
			{
				dbConnection.Open();

				SQLiteCommand sqlCommand = new SQLiteCommand("select * from Category order by name", dbConnection);
				SQLiteDataReader dr = sqlCommand.ExecuteReader();
				while (dr.Read())
				{
					Category category = new Category();
					category.Code = Convert.ToInt32(dr["code"]);
					category.Name = dr["name"].ToString();

					categories.Add(category);
				}

				dr.Close();
				dbConnection.Close();
			}

			return categories;
		}

		public Category GetByName(string categoryName)
		{
			Category category = null;

			using (SQLiteConnection dbConnection = _libraryDb.GetDbConnection())
			{
				dbConnection.Open();

				SQLiteCommand sqlCommand = new SQLiteCommand("select * from Category where Name=@Name", dbConnection);
				sqlCommand.Parameters.Add("@Name", DbType.String);
				sqlCommand.Parameters["@Name"].Value = categoryName;

				SQLiteDataReader dr = sqlCommand.ExecuteReader();

				if (dr.Read())
				{
					category = new Category();
					category.Code = Convert.ToInt32(dr["code"]);
					category.Name = dr["name"].ToString();
				}

				dr.Close();
				dbConnection.Close();
			}

			return category;
		}
	}
}
