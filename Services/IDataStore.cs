using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(object id);
        Task<T> GetItemAsync(object id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
