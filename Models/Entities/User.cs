using CarRepairApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CarRepairApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class User : BaseEntity, IDataErrorInfo
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

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<WorkProcess> WorkProcesses { get; set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Login))
                    if (string.IsNullOrWhiteSpace(Login))
                        return "Логин обязателен";
                    else
                        using (BaseModel model = new BaseModel())
                        {
                            if (model.Users
                                .Any(u =>
                                    u.Login.Equals(Login,
                                                   StringComparison.InvariantCulture)))
                            {
                                return "Логин занят!";
                            }
                        }
                if (columnName == nameof(LastName))
                    if (string.IsNullOrWhiteSpace(LastName))
                        return "Фамилия обязательна";
                if (columnName == nameof(FirstName))
                    if (string.IsNullOrWhiteSpace(FirstName))
                        return "Имя обязательно";
                if (columnName == nameof(UserRole))
                    if (Role == null)
                        return "Укажите роль";
                if (columnName == nameof(Password))
                    if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
                        return "Введите пароль длиной от 6 символов";
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                if (this[nameof(Login)] != null) return false;
                if (this[nameof(LastName)] != null) return false;
                if (this[nameof(FirstName)] != null) return false;
                if (this[nameof(Role)] != null) return false;
                if (this[nameof(Password)] != null) return false;
                return true;
            }
        }

        public string Error => throw new System.NotImplementedException();

        private string password;
        [NotMapped]
        public string Password
        {
            get => password;
            set
            {
                password = value;
                DependencyService
                    .Get<IHashGenerator>()
                    .GenerateHash(value,
                                  out byte[] passwordHash,
                                  out byte[] passwordSalt);
                PasswordHash = passwordHash;
                Salt = passwordSalt;
            }
        }
        [NotMapped]
        public string FullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Patronymic))
                {
                    return $"{LastName} {FirstName}";
                }
                else
                {
                    return $"{LastName} {FirstName} {Patronymic}";
                }
            }
        }
    }
}
