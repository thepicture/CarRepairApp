using CarRepairApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class PartDataStore : IDataStore<Part>
    {
        public Task<bool> AddItemAsync(Part item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Part> GetItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Part>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    return model.Parts
                        .Include(p => p.Car)
                        .Include(p => p.Provider)
                        .ToList();
                }
            });
        }

        public Task<bool> UpdateItemAsync(Part item)
        {
            throw new NotImplementedException();
        }
    }
}
