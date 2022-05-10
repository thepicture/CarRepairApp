using CarRepairApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class WorkProcessDataStore : IDataStore<WorkProcess>
    {
        public Task<bool> AddItemAsync(WorkProcess item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<WorkProcess> GetItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WorkProcess>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    return model.WorkProcesses
                        .Include(w => w.Car)
                        .Include(w => w.Customer)
                        .Include(w => w.Worker)
                        .Include(w => w.ProcessStage)
                        .ToList();
                }
            });
        }

        public Task<bool> UpdateItemAsync(WorkProcess item)
        {
            throw new NotImplementedException();
        }
    }
}
