using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Prueba.Models;
using Prueba.Models.Base;
using SQLite;

namespace Prueba.Services
{
    public class DataBaseService : IDataStore
    {
        readonly SQLiteAsyncConnection _database;
        private object locker = new object();

        public DataBaseService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserInfo>().Wait();
            //_database.CreateTableAsync<Library>().Wait();           
        }
        public Task<int> AddItemAsync<T>(T item) where T : TableBase
        {
            lock (locker)
            {
                return _database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync<T>(T item)
        {
            return _database.DeleteAsync(item);
        }

        public Task<T> GetItemAsync<T>(int id) where T : TableBase, new()
        {
            return _database.Table<T>().FirstOrDefaultAsync(key=> key.Id == id);
        }

        public Task<List<T>> GetItemsAsync<T>() where T : class, new()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<int> UpdateItemAsync<T>(T item)
        {
            lock (locker)
            {
                return _database.UpdateAsync(item);
            }
        }

    }
}