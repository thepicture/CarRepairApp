using CarRepairApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class RegistrationUserDataStore : IDataStore<RegistrationUser>
    {
        public Task<bool> AddItemAsync(RegistrationUser item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationUser> GetItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RegistrationUser>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(RegistrationUser item)
        {
            throw new NotImplementedException();
        }
    }
}
