using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRepairApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class Car : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        public ICollection<WorkProcess> WorkProcesses { get; set; }
    }
}
