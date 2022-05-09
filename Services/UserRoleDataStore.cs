using CarRepairApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class UserRoleDataStore : IDataStore<UserRole>
    {
        public Task<bool> AddItemAsync(UserRole item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> GetItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserRole>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    return model.UserRoles.ToList();
                }
            });
        }

        public Task<bool> UpdateItemAsync(UserRole item)
        {
            throw new NotImplementedException();
        }
    }
}
