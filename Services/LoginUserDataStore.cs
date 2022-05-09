using CarRepairApp.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public class LoginUserDataStore : IDataStore<LoginUser>
    {
        public async Task<bool> AddItemAsync(LoginUser item)
        {
            return await Task.Run(() =>
            {
                using (BaseModel model = new BaseModel())
                {
                    if (!string.IsNullOrWhiteSpace(item.Login)
                        && !string.IsNullOrWhiteSpace(item.Password)
                        && model.Users
                            .Include(u => u.Role)
                            .AsNoTracking()
                            .FirstOrDefault(u => u.Login == item.Login)
                                is User currentUser)
                    {
                        DependencyService
                        .Get<IHashGenerator>()
                        .GenerateHash(item.Password,
                                      out byte[] hash,
                                      out byte[] salt,
                                      inputSalt: currentUser.Salt);
                        if (Enumerable.SequenceEqual(currentUser.PasswordHash, hash))
                        {
                            currentUser.Password = item.Password;
                            DependencyService
                            .Get<IIdentityService<User>>()
                            .WeakIdentity = currentUser;
                            DependencyService
                                .Get<IMessageService>()
                                .InformAsync($"Вы вошли как {currentUser.Role.Title.ToLower()}");
                            return true;
                        }
                    }
                }
                DependencyService
                    .Get<IMessageService>()
                    .InformErrorAsync($"Неверный логин или пароль");
                return false;
            });
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
