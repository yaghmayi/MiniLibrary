using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using MiniLibrary.DataAccess.Base;
using MiniLibrary.DataAccess.Interfaces;
using MiniLibrary.Models;

namespace MiniLibrary.DataAccess
{
	public class ItemRepository : IItemRepository
	{
		private LibraryDb _libraryDb;

		public ItemRepository()
		{
			_libraryDb = new LibraryDb();
			_libraryDb.Initialize();
		}

		public List<Item> GetAll()
		{
			List<Item> items = new List<Item>();

			using (SQLiteConnection dbConnection = _libraryDb.GetDbConnection())
			{
				dbConnection.Open();

				string sql = _libraryDb.GetScript("Select_All_Items.sql");
				SQLiteCommand sqlCommand = new SQLiteCommand(sql, dbConnection);
				SQLiteDataReader dr = sqlCommand.ExecuteReader();
				while (dr.Read())
				{
					Item item = new Item();
					item.Id = Convert.ToInt32(dr["item_id"]);
					item.Name = Convert.ToString(dr["item_name"].ToString());

					item.Category = new Category();
					item.Category.Code = Convert.ToInt32(dr["category_code"]);
					item.Category.Name = Convert.ToString(dr["category_name"]);

					items.Add(item);
				}

				dr.Close();
				dbConnection.Close();
			}

			return items;
		}

		public Item GetById(int id)
		{
			Item item = null;

			using (SQLiteConnection dbConnection = _libraryDb.GetDbConnection())
			{
				dbConnection.Open();

				string sql = _libraryDb.GetScript("Select_Item_ById.sql");
				SQLiteCommand sqlCommand = new SQLiteCommand(sql, dbConnection);
				sqlCommand.Parameters.Add("@ItemId", DbType.Int32);
				sqlCommand.Parameters["@ItemId"].Value = id;

				SQLiteDataReader dr = sqlCommand.ExecuteReader();
				if (dr.Read())
				{
					item = new Item();
					item.Id = Convert.ToInt32(dr["item_id"]);
					item.Name = Convert.ToString(dr["item_name"].ToString());

					item.Category = new Category();
					item.Category.Code = Convert.ToInt32(dr["category_code"]);
					item.Category.Name = Convert.ToString(dr["category_name"]);
				}

				dr.Close();

				dbConnection.Close();
			}

			return item;
		}

		public void Add(Item item)
		{
			using (SQLiteConnection dbConnection = _libraryDb.GetDbConnection())
			{
				dbConnection.Open();

				SQLiteCommand sqlCommand = new SQLiteCommand("insert into Item (Id,Name,CategoryCode) values (@Id,@Name,@CategoryCode)", dbConnection);

				sqlCommand.Parameters.Add("@Id", DbType.Int32);
				sqlCommand.Parameters.Add("@Name", DbType.String);
				sqlCommand.Parameters.Add("@CategoryCode", DbType.Int32);
				sqlCommand.Parameters["@Id"].Value = item.Id;
				sqlCommand.Parameters["@Name"].Value = item.Name;
				sqlCommand.Parameters["@CategoryCode"].Value = item.Category.Code;

				sqlCommand.ExecuteNonQuery();
				dbConnection.Close();
			}
		}

		public void Update(Item item)
		{
			using (SQLiteConnection dbConnection = _libraryDb.GetDbConnection())
			{
				dbConnection.Open();

				SQLiteCommand sqlCommand = new SQLiteCommand("update Item set Name=@Name, CategoryCode=@CategoryCode where Id=@Id", dbConnection);

				sqlCommand.Parameters.Add("@Id", DbType.Int32);
				sqlCommand.Parameters.Add("@Name", DbType.String);
				sqlCommand.Parameters.Add("@CategoryCode", DbType.Int32);
				sqlCommand.Parameters["@Id"].Value = item.Id;
				sqlCommand.Parameters["@Name"].Value = item.Name;
				sqlCommand.Parameters["@CategoryCode"].Value = item.Category.Code;

				sqlCommand.ExecuteNonQuery();

				dbConnection.Close();
			}
		}

		public void Delete(int id)
		{
			using (SQLiteConnection dbConnection = _libraryDb.GetDbConnection())
			{
				dbConnection.Open();

				SQLiteCommand sqlCommand = new SQLiteCommand("delete from Item where Id=@Id", dbConnection);
				sqlCommand.Parameters.Add("@Id", DbType.Int32);
				sqlCommand.Parameters["@Id"].Value = id;

				sqlCommand.ExecuteNonQuery();

				dbConnection.Close();
			}
		}

		public int GetNextId()
		{
			int nextId = 1;

			using (SQLiteConnection dbConnection = _libraryDb.GetDbConnection())
			{
				dbConnection.Open();

				SQLiteCommand sqlCommand = new SQLiteCommand("select max(id) from Item", dbConnection);

				nextId = Convert.ToInt32(sqlCommand.ExecuteScalar()) + 1;

				dbConnection.Close();
			}

			return nextId;
		}
	}
}
