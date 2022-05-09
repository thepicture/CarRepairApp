using System.ComponentModel.DataAnnotations.Schema;

namespace CarRepairApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    [NotMapped]
    public class PasswordedUser : User
    {
        public string Password { get; set; }
    }
}
