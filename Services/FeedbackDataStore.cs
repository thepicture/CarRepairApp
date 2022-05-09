using CarRepairApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class FeedbackDataStore : IDataStore<Feedback>
    {
        public async Task<bool> AddItemAsync(Feedback item)
        {
            if (!item.IsValid)
            {
                await DependencyService
                    .Get<IMessageService>()
                    .InformErrorAsync("Исправьте ошибки полей " +
                    "перед сохранением обратной связи");
                return false;
            }
            item.PostingDate = DateTime.Now;
            item.PosterId = DependencyService
                .Get<IIdentityService<User>>().WeakIdentity.Id;
            item.ReceiverId = item.Receiver.Id;
            item.Receiver = null;
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    model.Feedbacks.Add(item);
                    try
                    {
                        model.SaveChanges();
                        DependencyService
                            .Get<IMessageService>()
                            .InformAsync("Вы отправили обратную связь");
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

        public Task<Feedback> GetItemAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Feedback>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    return model.Feedbacks
                        .Include(f => f.Poster)
                        .Include(f => f.Receiver)
                        .ToList();
                }
            });
        }

        public Task<bool> UpdateItemAsync(Feedback item)
        {
            throw new NotImplementedException();
        }
    }
}
