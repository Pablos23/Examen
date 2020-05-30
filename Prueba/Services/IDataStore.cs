using Prueba.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prueba.Services
{
    public interface IDataStore
    {
        Task<int> AddItemAsync<T>(T item) where T : TableBase;
        Task<int> UpdateItemAsync<T>(T item);
        Task<int> DeleteItemAsync<T>(T item);
        Task<T> GetItemAsync<T>(int id) where T : TableBase, new();
        Task<List<T>> GetItemsAsync<T>() where T : class, new();
    }
}
