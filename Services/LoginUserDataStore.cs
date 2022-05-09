using CarRepairApp.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class LoginUserDataStore : IDataStore<LoginUser>
    {
        public Task<bool> AddItemAsync(LoginUser item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<LoginUser> GetItemAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<LoginUser>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(LoginUser item)
        {
            throw new System.NotImplementedException();
        }
    }
}
