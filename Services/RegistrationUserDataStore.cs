using CarRepairApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class RegistrationUserDataStore : IDataStore<RegistrationUser>
    {
        public async Task<bool> AddItemAsync(RegistrationUser item)
        {
            if (!item.IsValid)
            {
                await DependencyService
                    .Get<IMessageService>()
                    .InformErrorAsync("Исправьте ошибки полей " +
                    "перед сохранением пользователя");
                return false;
            }
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    model.Entry(item.Role).State = System.Data.Entity.EntityState.Unchanged;
                    model.Users.Add(item);
                    try
                    {
                        model.SaveChanges();
                        DependencyService
                            .Get<IMessageService>()
                            .InformAsync("Вы зарегистрированы");
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
