using CarRepairApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class CarDataStore : IDataStore<Car>
    {
        public Task<bool> AddItemAsync(Car item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Car> GetItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Car>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    return model.Cars.ToList();
                }
            });
        }

        public Task<bool> UpdateItemAsync(Car item)
        {
            throw new NotImplementedException();
        }
    }
}
