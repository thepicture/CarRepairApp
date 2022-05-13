using CarRepairApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarRepairApp.Models.Entities
{
    public class BaseModel : DbContext
    {
        public BaseModel()
            : base(App.CurrentConnectionString)
        {
            if (Database.Connection.ConnectionString.Contains("master"))
            {
                Database.Connection.Open();
                if (Database.Connection.State != ConnectionState.Open)
                {
                    throw new Exception("Invalid connection string");
                }
            }
            else
            {
                InitializeDatabase();
            }
        }

        private void InitializeDatabase()
        {
            if (!Database.Exists())
            {
                Database.Initialize(true);
                Seed(this)
                    .Wait();
            }
        }

        public async Task Seed(BaseModel context)
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

            IList<Provider> providers = new List<Provider>
            {
                new Provider{Title = "ООО \"Ремонтник\""},
                new Provider{Title = "ООО \"Каскад\""},
                new Provider{Title = "ООО \"Part First\""},
                new Provider{Title = "ООО \"DropIfCarChanges\""}
            };
            context.Providers.AddRange(providers);
            context.SaveChanges();

            IList<Part> parts = new List<Part>();
            var part1 = new Part
            {
                Title = "Фары простые",
                BaseCode = "A123BC",
                PriceInRubles = 2000,
                Description = "Не ломаются со временем",
                ProviderId = 1,
                CarId = 1
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/104/272641329_29ba94d516_b.jpg");
                part1.Photo = response;
            }
            var part2 = new Part
            {
                Title = "Красный бампер",
                BaseCode = "A123CB",
                PriceInRubles = 7000,
                Description = "Открывается с третьего раза",
                ProviderId = 2,
                CarId = 2
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/26/36662094_76038fd239_b.jpg");
                part2.Photo = response;
            }
            var part3 = new Part
            {
                Title = "Передние фары",
                BaseCode = "A321BC",
                PriceInRubles = 2500,
                Description = "Свет от фар искуственный, поэтому мелькает",
                ProviderId = 3,
                CarId = 3
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/28/36658545_1513eb9a7b_b.jpg");
                part3.Photo = response;
            }
            var part4 = new Part
            {
                Title = "Замена номера",
                BaseCode = "A231BC",
                PriceInRubles = 2000,
                Description = "Только блатные номера",
                ProviderId = 4,
                CarId = 2
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/21/36799670_809b153976_b.jpg");
                part4.Photo = response;
            }
            var part5 = new Part
            {
                Title = "Прочный бампер",
                BaseCode = "B123CB",
                PriceInRubles = 3500,
                Description = "Умеет выдерживать любые удары, но гнётся от кочек на дороге",
                ProviderId = 3,
                CarId = 4
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/26/36662094_76038fd239_b.jpg");
                part5.Photo = response;
            }
            var part6 = new Part
            {
                Title = "Фары с примесью фосфора",
                BaseCode = "A321DC",
                PriceInRubles = 1500,
                Description = "Топливо и электричество в машине не обязательно",
                ProviderId = 1,
                CarId = 4
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/28/36658545_1513eb9a7b_b.jpg");
                part6.Photo = response;
            }
            var part7 = new Part
            {
                Title = "Корпус классический",
                BaseCode = "A113BC",
                PriceInRubles = 2600,
                Description = "Выдерживает кочки и не марается",
                ProviderId = 2,
                CarId = 4
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/104/272641329_29ba94d516_b.jpg");
                part7.Photo = response;
            }
            var part8 = new Part
            {
                Title = "Томатный бампер",
                BaseCode = "B321CB",
                PriceInRubles = 5000,
                Description = "Элитный и с необычным названием",
                ProviderId = 4,
                CarId = 1
            };
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client
                    .GetByteArrayAsync("https://live.staticflickr.com/26/36662094_76038fd239_b.jpg");
                part8.Photo = response;
            }
            parts.Add(part1);
            parts.Add(part2);
            parts.Add(part3);
            parts.Add(part4);
            parts.Add(part5);
            parts.Add(part6);
            parts.Add(part7);
            parts.Add(part8);
            context.Parts.AddRange(parts);
            context.SaveChanges();

            DependencyService
                .Get<IMessageService>()
                .InformAsync("База данных установлена со строкой подключения "
                + Database.Connection.ConnectionString
                + ". Сохраните её для устранения возможных проблем");
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProcessStage> ProcessStages { get; set; }
        public virtual DbSet<WorkProcess> WorkProcesses { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<UserRole>()
                .HasMany(r => r.Users)
                .WithRequired(u => u.Role)
                .HasForeignKey(u => u.RoleId);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Feedbacks)
                .WithRequired(u => u.Poster)
                .HasForeignKey(u => u.PosterId)
                .WillCascadeOnDelete(false);
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Feedbacks)
                .WithRequired(u => u.Receiver)
                .HasForeignKey(u => u.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.WorkProcesses)
                .WithRequired(u => u.Worker)
                .HasForeignKey(u => u.WorkerId)
                .WillCascadeOnDelete(false);
            modelBuilder
                .Entity<Customer>()
                .HasMany(u => u.WorkProcesses)
                .WithRequired(u => u.Customer)
                .HasForeignKey(u => u.CustomerId)
                .WillCascadeOnDelete(false);
            modelBuilder
                .Entity<ProcessStage>()
                .HasMany(p => p.WorkProcesses)
                .WithRequired(u => u.ProcessStage)
                .HasForeignKey(u => u.ProcessStageId)
                .WillCascadeOnDelete(false);

            modelBuilder
               .Entity<Car>()
               .HasMany(c => c.WorkProcesses)
               .WithRequired(w => w.Car)
               .HasForeignKey(u => u.CarId)
               .WillCascadeOnDelete(false);

            modelBuilder
                .Entity<Provider>()
                .HasMany(p => p.Parts)
                .WithRequired(p => p.Provider)
                .HasForeignKey(u => u.ProviderId)
                .WillCascadeOnDelete(false);
            modelBuilder
                .Entity<Car>()
                .HasMany(c => c.Parts)
                .WithRequired(p => p.Car)
                .HasForeignKey(u => u.CarId)
                .WillCascadeOnDelete(false);
        }
    }
}