using CarRepairApp.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;

namespace CarRepairApp.Models.Entities
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<BaseModel>
    {
        public override void InitializeDatabase(BaseModel context)
        {
            context.Database.CreateIfNotExists();
            context.Database.ExecuteSqlCommand(
                TransactionalBehavior.DoNotEnsureTransaction,
                string.Format(
                    "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                    context.Database.Connection.Database));

            base.InitializeDatabase(context);
        }

        protected async override void Seed(BaseModel context)
        {
            IList<UserRole> roles = new List<UserRole>
            {
                new UserRole { Title = "Администратор" },
                new UserRole { Title = "Работник" }
            };
            context.UserRoles.AddRange(roles);

            IList<User> users = new List<User>();
            User adminUser = new User
            {
                Login = "admin",
                LastName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                RoleId = 1
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/107/311380970_47942e7a36_b.jpg");
                adminUser.Photo = response;
            }
            DependencyService
                .Get<IHashGenerator>()
                .GenerateHash(inputString: "123456",
                              out byte[] adminPasswordHash,
                              out byte[] adminSalt);
            adminUser.PasswordHash = adminPasswordHash;
            adminUser.Salt = adminSalt;
            users.Add(adminUser);
            User workerUser = new User
            {
                Login = "worker",
                LastName = "Петров",
                FirstName = "Пётр",
                Patronymic = "Петрович",
                RoleId = 2
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/8369/8519701982_0f394dbec1_b.jpg");
                workerUser.Photo = response;
            }
            DependencyService
                .Get<IHashGenerator>()
                .GenerateHash(inputString: "123456",
                              out byte[] workerPasswordHash,
                              out byte[] workerSalt);
            workerUser.PasswordHash = workerPasswordHash;
            workerUser.Salt = workerSalt;
            users.Add(workerUser);
            context.Users.AddRange(users);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
