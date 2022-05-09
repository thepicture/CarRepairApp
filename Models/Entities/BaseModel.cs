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
        }
    }
}