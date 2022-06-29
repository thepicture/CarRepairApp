using CarRepairApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class UserDataStore : IDataStore<User>
    {
        public Task<bool> AddItemAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetItemAsync(object id)
        {
            using (BaseModel model = new BaseModel())
            {
                return model.Users.AsNoTracking().Include(u => u.Role).FirstAsync(u => u.Id == (int)id);
            }
        }

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    return model.Users.ToList();
                }
            });
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            if (!item.IsValid)
            {
                await DependencyService
                    .Get<IMessageService>()
                    .InformErrorAsync("Исправьте ошибки полей " +
                    "перед сохранением");
                return false;
            }
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    User currentUser = model.Users.Find(item.Id);
                    model.Entry(currentUser).CurrentValues.SetValues(item);
                    try
                    {
                        model.SaveChanges();
                        DependencyService
                            .Get<IMessageService>()
                            .InformAsync("Профиль обновлён");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        DependencyService
                            .Get<IMessageService>()
                            .InformErrorAsync(ex);
                        return false;
                    }
                }
            });
        }
    }
}
