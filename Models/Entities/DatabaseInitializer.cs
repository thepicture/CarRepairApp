﻿using CarRepairApp.Services;
using System;
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
            context.SaveChanges();

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

            IList<Feedback> feedbacks = new List<Feedback>
            {
                new Feedback
                {
                    PostingDate = DateTime.Now
                        .AddDays(-3)
                        .AddHours(9)
                        .AddMinutes(30),
                    Message = "Занесешь завтра фары модели А?",
                    PosterId = 1,
                    ReceiverId = 2
                },
                new Feedback
                {
                    PostingDate = DateTime.Now
                        .AddDays(-3)
                        .AddHours(9)
                        .AddMinutes(50),
                    Message = "Добрый день",
                    PosterId = 2,
                    ReceiverId = 1
                },
                new Feedback
                {
                    PostingDate = DateTime.Now
                        .AddDays(-3)
                        .AddHours(9)
                        .AddMinutes(55),
                    Message = "Попробую",
                    PosterId = 2,
                    ReceiverId = 1
                },
                new Feedback
                {
                    PostingDate = DateTime.Now
                        .AddDays(-3)
                        .AddHours(30)
                        .AddMinutes(30),
                    Message = "Давай",
                    PosterId = 1,
                    ReceiverId = 2
                },
                new Feedback
                {
                    PostingDate = DateTime.Now
                        .AddDays(-3)
                        .AddHours(40)
                        .AddMinutes(30),
                    Message = "Прихвати незамерзайку",
                    PosterId = 1,
                    ReceiverId = 2
                },
            };

            context.Feedbacks.AddRange(feedbacks);
            context.SaveChanges();

            IList<ProcessStage> processStages = new List<ProcessStage>
            {
                new ProcessStage { Title = "Отменён" },
                new ProcessStage { Title = "В обработке" },
                new ProcessStage { Title = "Выполняется" },
                new ProcessStage { Title = "Выполнен" }
            };
            context.ProcessStages.AddRange(processStages);
            context.SaveChanges();

            IList<Car> cars = new List<Car>
            {
                new Car { Model = "Mazda A5" },
                new Car { Model = "Toyota Supra" },
                new Car { Model = "Audi GT" },
                new Car { Model = "Ferrari B5" }
            };
            context.Cars.AddRange(cars);
            context.SaveChanges();

            IList<Customer> customers = new List<Customer>();

            var customer1 = new Customer
            {
                LastName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                PhoneNumber = "71111111111"
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/2571/3764803987_213de495bc_b.jpg");
                customer1.Photo = response;
            }
            customers.Add(customer1);

            var customer2 = new Customer
            {
                LastName = "Александров",
                FirstName = "Александр",
                Patronymic = "Александрович",
                PhoneNumber = "72222222222"
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/3929/15572153942_f5befc6041_b.jpg");
                customer2.Photo = response;
            }
            customers.Add(customer2);
            context.Customers.AddRange(customers);
            context.SaveChanges();

            IList<WorkProcess> workProcesses = new List<WorkProcess>
            {
                new WorkProcess
                {
                    Title = "Ремонт кузова",
                    StartOfWork = DateTime.Now.AddDays(-8),
                    PriceInRubles = 1500,
                    WorkerId = 1,
                    CustomerId = 1,
                    ProcessStageId = 1,
                    CarId=1
                },
                new WorkProcess
                {
                    Title = "Ремонт двигателя",
                    StartOfWork = DateTime.Now.AddDays(-7),
                    PriceInRubles = 1200,
                    WorkerId = 1,
                    CustomerId = 2,
                    ProcessStageId = 2,
                    CarId=2
                },
                new WorkProcess
                {
                    Title = "Замена подшипников",
                    StartOfWork = DateTime.Now.AddDays(-6),
                    PriceInRubles = 2000,
                    WorkerId = 1,
                    CustomerId = 2,
                    ProcessStageId = 3,
                    CarId=3
                },
                new WorkProcess
                {
                    Title = "Ремонт фар",
                    StartOfWork = DateTime.Now.AddDays(-5),
                    PriceInRubles = 4000,
                    WorkerId = 1,
                    CustomerId = 1,
                    ProcessStageId = 4,
                    CarId=4
                },
                new WorkProcess
                {
                    Title = "Замена фар",
                    StartOfWork = DateTime.Now.AddDays(-5),
                    PriceInRubles = 5000,
                    WorkerId = 1,
                    CustomerId = 2,
                    ProcessStageId = 1,
                    CarId=1
                },
                new WorkProcess
                {
                    Title = "Замена корпуса",
                    StartOfWork = DateTime.Now.AddDays(-4),
                    PriceInRubles = 2000,
                    WorkerId = 1,
                    CustomerId = 2,
                    ProcessStageId = 2,
                    CarId=2
                },
                new WorkProcess
                {
                    Title = "Исправление выхлопной трубы",
                    StartOfWork = DateTime.Now.AddDays(-3),
                    PriceInRubles = 7000,
                    WorkerId = 1,
                    CustomerId = 1,
                    ProcessStageId = 3,
                    CarId=3
                },
                new WorkProcess
                {
                    Title = "Замена гудка",
                    StartOfWork = DateTime.Now.AddDays(-2),
                    PriceInRubles = 6700,
                    WorkerId = 1,
                    CustomerId = 2,
                    ProcessStageId = 4,
                    CarId=4
                },
                new WorkProcess
                {
                    Title = "Ремонт двигателя",
                    StartOfWork = DateTime.Now.AddDays(-1),
                    PriceInRubles = 6666,
                    WorkerId = 1,
                    CustomerId = 2,
                    ProcessStageId = 1,
                    CarId=1
                }
            };
            context.WorkProcesses.AddRange(workProcesses);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
