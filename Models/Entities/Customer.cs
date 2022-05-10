using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRepairApp.Models.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Patronymic { get; set; }
        public byte[] Photo { get; set; }
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

        public ICollection<WorkProcess> WorkProcesses { get; set; }

        [NotMapped]
        public string FormattedPhoneNumber
        {
            get
            {
                return $"+{PhoneNumber[0]} " +
                    $"({PhoneNumber[1]}{PhoneNumber[2]}{PhoneNumber[3]}) " +
                    $"{PhoneNumber[4]}{PhoneNumber[5]}{PhoneNumber[6]}-" +
                    $"{PhoneNumber[7]}{PhoneNumber[8]}-" +
                    $"{PhoneNumber[9]}{PhoneNumber[10]}";
            }
        }
    }
}
