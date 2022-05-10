using CarRepairApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class CustomerDataStore : IDataStore<Customer>
    {
        public Task<bool> AddItemAsync(Customer item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    return model.Customers.ToList();
                }
            });
        }

        public Task<bool> UpdateItemAsync(Customer item)
        {
            throw new NotImplementedException();
        }
    }
}
