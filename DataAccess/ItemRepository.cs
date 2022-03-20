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
					item.Id = GetColumnValue<int>(dr, "item_id");
					item.Name = GetColumnValue<string>(dr, "item_name");
					item.Author = GetColumnValue<string>(dr, "item_author");
					item.Description = GetColumnValue<string>(dr, "item_description");

					item.Category = new Category();
					item.Category.Code = GetColumnValue<int>(dr, "category_code");
					item.Category.Name = GetColumnValue<string>(dr, "category_name");

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
					item.Id = GetColumnValue<int>(dr, "item_id");
					item.Name = GetColumnValue<string>(dr, "item_name");
					item.Author = GetColumnValue<string>(dr, "item_author");
					item.Description = GetColumnValue<string>(dr, "item_description");

					item.Category = new Category();
					item.Category.Code = GetColumnValue<int>(dr, "category_code");
					item.Category.Name = GetColumnValue<string>(dr, "category_name");
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

				string sql = _libraryDb.GetScript("Insert_Item.sql");
				SQLiteCommand sqlCommand = new SQLiteCommand(sql, dbConnection);

				sqlCommand.Parameters.Add("@Id", DbType.Int32);
				sqlCommand.Parameters.Add("@Name", DbType.String);
				sqlCommand.Parameters.Add("@CategoryCode", DbType.Int32);
				sqlCommand.Parameters.Add("@Author", DbType.String);
				sqlCommand.Parameters.Add("@Description", DbType.String);
				sqlCommand.Parameters["@Id"].Value = item.Id;
				sqlCommand.Parameters["@Name"].Value = item.Name;
				sqlCommand.Parameters["@CategoryCode"].Value = item.Category.Code;
				sqlCommand.Parameters["@Author"].Value = item.Author;
				sqlCommand.Parameters["@Description"].Value = item.Description;

				sqlCommand.ExecuteNonQuery();
				dbConnection.Close();
			}
		}

		public void Update(Item item)
		{
			using (SQLiteConnection dbConnection = _libraryDb.GetDbConnection())
			{
				dbConnection.Open();

				string sql = _libraryDb.GetScript("Update_Item.sql");
				SQLiteCommand sqlCommand = new SQLiteCommand(sql, dbConnection);

				sqlCommand.Parameters.Add("@Id", DbType.Int32);
				sqlCommand.Parameters.Add("@Name", DbType.String);
				sqlCommand.Parameters.Add("@CategoryCode", DbType.Int32);
				sqlCommand.Parameters.Add("@Author", DbType.String);
				sqlCommand.Parameters.Add("@Description", DbType.String);
				sqlCommand.Parameters["@Id"].Value = item.Id;
				sqlCommand.Parameters["@Name"].Value = item.Name;
				sqlCommand.Parameters["@CategoryCode"].Value = item.Category.Code;
				sqlCommand.Parameters["@Author"].Value = item.Author;
				sqlCommand.Parameters["@Description"].Value = item.Description;

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

		private T GetColumnValue<T>(SQLiteDataReader dataReader, string columnName)
		{
			T columnValue = default(T);

			if (dataReader[columnName] != DBNull.Value)
			{
				columnValue = (T) Convert.ChangeType(dataReader[columnName], typeof(T));
			}

			return columnValue;
		}
	}
}
