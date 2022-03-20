using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace MiniLibrary.DataAccess.Base
{
    public class LibraryDb
    {
        private static bool _initialized = false;
        private string _databaseName = "MiniLibrary";

        public void Initialize()
        {
            if (!_initialized)
            {
                using (SQLiteConnection dbConnection = GetDbConnection())
                {
                    dbConnection.Open();

                    string sql = GetScript("CreateDb.sql");
                    SQLiteCommand sqlCommand = new SQLiteCommand(sql, dbConnection);
                    sqlCommand.ExecuteNonQuery();

                    sql = GetScript("InitData.sql");
                    sqlCommand = new SQLiteCommand(sql, dbConnection);
                    sqlCommand.ExecuteNonQuery();

                    dbConnection.Close();

                    _initialized = true;
                }
            }
        }

        public int GetRowsCount(string tableName)
        {
            int count = 0;

            using (SQLiteConnection dbConnection = GetDbConnection())
            {
                dbConnection.Open();

                string sql = $"select count(*) from {tableName}";
                SQLiteCommand sqlCommand = new SQLiteCommand(sql, dbConnection);

                count = Convert.ToInt32(sqlCommand.ExecuteScalar());

                dbConnection.Close();
            }

            return count;
        }

        public SQLiteConnection GetDbConnection()
        {
            SQLiteConnection dbConnection = new SQLiteConnection($"Data Source={_databaseName}");

            return dbConnection;
        }

        public string GetScript(string scriptFileName)
        {
            string resourceName = $"MiniLibrary.DataAccess.Base.{scriptFileName}";

            var assembly = Assembly.GetExecutingAssembly();
            string script = null;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    script = reader.ReadToEnd();
                }
            }

            return script;
        }
    }
}
