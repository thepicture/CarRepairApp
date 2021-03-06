using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRepairApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class UserRole : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
