using CarRepairApp.Models.Entities;
using CarRepairApp.Properties;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CarRepairApp.Services
{
    class UserIdentityService : IIdentityService<User>
    {
        public User StrongIdentity
        {
            get => JsonConvert.DeserializeObject<User>(
                    Encoding.UTF8.GetString(
                        Convert.FromBase64String(
                            Settings.Default.SerializedIdentity)),
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling
                                    .Ignore
                        });

            set
            {
                string serializedUser = JsonConvert
                    .SerializeObject(value, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                byte[] serializedUserBytes = Encoding.UTF8.GetBytes(
                                  serializedUser);
                string encryptedIdentity = Convert.ToBase64String(serializedUserBytes);
                Settings.Default.SerializedIdentity = encryptedIdentity;
                Settings.Default.Save();
            }
        }

        public User WeakIdentity { get; set; }

        public void Logout()
        {
            Settings.Default.SerializedIdentity = string.Empty;
            Settings.Default.Save();
            WeakIdentity = null;
        }
    }
}
