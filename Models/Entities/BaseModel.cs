using System.Data.Entity;

namespace CarRepairApp.Models.Entities
{
    public class BaseModel : DbContext
    {
        public BaseModel()
            : base("name=BaseModel")
        {
            Database.Initialize(false);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProcessStage> ProcessStages { get; set; }
        public virtual DbSet<WorkProcess> WorkProcesses { get; set; }
        public virtual DbSet<Car> Cars { get; set; }

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
        }
    }
}