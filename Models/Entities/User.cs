using System.ComponentModel.DataAnnotations;

namespace CarRepairApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class User : BaseEntity
    {
        [MaxLength(100)]
        [Required]
        public string Login { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string Patronymic { get; set; }
        [Required]
        [MaxLength(512)]
        public byte[] PasswordHash { get; set; }
        [Required]
        [MaxLength(512)]
        public byte[] Salt { get; set; }
        public byte[] Photo { get; set; }

        public int RoleId { get; set; }

        public virtual UserRole Role { get; set; }
    }
}
